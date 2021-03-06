﻿using Lab5.Ast;
using Lab5.Ast.Expressions;
using Lab5.Ast.Statements;
using Lab5.Interpreting.Values;
using Lab5.Interpreting.Values.Functions;
using Lab5.Parsing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static System.Diagnostics.Debug;
namespace Lab5.Interpreting
{
    class VarBackup
    {
        public string name;
        public object value;
        public bool didNotExist;

        public VarBackup(string name, object value, bool didNotExist)
        {
            this.name = name;
            this.value = value;
            this.didNotExist = didNotExist;
        }
    }
    sealed class Interpreter : IStatementVisitor, IExpressionVisitor<object>
    {
        readonly string source;
        Dictionary<string, object> variables = new Dictionary<string, object>();
        Stack<VarBackup> oldVariables = new Stack<VarBackup>();
        Stack<int> varCounts = new Stack<int>();
        public Interpreter(string source, IReadOnlyDictionary<string, object> customVariables)
        {
            this.source = source;
            Dictionary<string, object> myVariables = new Dictionary<string, object> {
                { "true", true },
                { "false", false },
                { "null", null },
                { "dump", new DumpFunction() },
                { "trace", new TraceFunction() },
            };
            foreach (var kv in customVariables)
            {
                myVariables[kv.Key] = kv.Value;
            }
            foreach (var kv in myVariables)
            {
                variables[kv.Key] = (kv.Value);
            }
        }
        public void RunProgram(ProgramNode program)
        {
            foreach (var statement in program.Statements)
            {
                Run(statement);
            }
        }
        void RunBlock(Block block)
        {
            varCounts.Push(0);
            foreach (var statement in block.Statements)
            {
                Run(statement);
            }
            int varDel = varCounts.Pop();
            for (int i = 0; i < varDel; i++)
            {
                VarBackup varBackup = oldVariables.Pop();
                variables[varBackup.name] = varBackup.value;
            }
        }
        #region statements
        void Run(IStatement statement)
        {
            statement.Accept(this);
        }
        public void VisitIf(IfStatement ifStatement)
        {
            if (Calc<bool>(ifStatement.Condition))
            {
                RunBlock(ifStatement.Body);
            }
        }
        public void VisitWhile(WhileStatement whileStatement)
        {
            while (Calc<bool>(whileStatement.Condition))
            {
                RunBlock(whileStatement.Body);
            }
        }
        public void VisitExpressionStatement(ExpressionStatement expressionStatement)
        {
            Calc(expressionStatement.Expr);
        }
        public void VisitAssignment(Assignment assignment)
        {
            if (variables.ContainsKey(assignment.Variable))
            {
                object expression = Calc(assignment.Expr);
                oldVariables.Push(new VarBackup(assignment.Variable, variables[assignment.Variable], false));
                variables[assignment.Variable] = expression;
            }
            else
            {
                throw MakeError(assignment, $"Переменная не задана");
            }
        }
        public void VisitNewAssignment(NewAssignment newAssignment)
        {

            object expression = Calc(newAssignment.Expr);
            if (!variables.ContainsKey(newAssignment.Variable))
            {
                oldVariables.Push(new VarBackup(newAssignment.Variable, 0, true));
            }
            else oldVariables.Push(new VarBackup(newAssignment.Variable, variables[newAssignment.Variable], false));
            variables[newAssignment.Variable] = expression;

            if (varCounts.Count == 0)
                varCounts.Push(1);
            else
                varCounts.Push(varCounts.Pop() + 1);
        }
        #endregion
        #region expressions
        object Calc(IExpression expression)
        {
            return expression.Accept(this);
        }
        T Calc<T>(IExpression expression)
        {
            var value = Calc(expression);
            if (!(value is T))
            {
                throw MakeError(expression, $"Ожидали {typeof(T)}, получили {value}");
            }
            return (T)value;
        }
        public object VisitBinary(Binary binary)
        {
            switch (binary.Operator)
            {
                case BinaryOperator.Addition:
                    return CalcAddition(binary);
                case BinaryOperator.Subtraction:
                    return CalcSubtraction(binary);
                case BinaryOperator.Multiplication:
                    return CalcMultiplication(binary);
                case BinaryOperator.Division:
                    return CalcDivision(binary);
                case BinaryOperator.Remainder:
                    return CalcReminder(binary);
                case BinaryOperator.Equal:
                    return CalcEqual(binary);
                case BinaryOperator.Less:
                    return CalcLess(binary);
                default:
                    throw MakeError(binary, $"Неизвестная операция {binary.Operator}");
            }
        }
        #region binary operations
        object CalcAddition(Binary binary)
        {
            Assert(binary.Operator == BinaryOperator.Addition);
            return Calc<int>(binary.Left) + Calc<int>(binary.Right);
        }
        object CalcSubtraction(Binary binary)
        {
            Assert(binary.Operator == BinaryOperator.Subtraction);
            return Calc<int>(binary.Left) - Calc<int>(binary.Right);
        }
        object CalcMultiplication(Binary binary)
        {
            Assert(binary.Operator == BinaryOperator.Multiplication);
            return Calc<int>(binary.Left) * Calc<int>(binary.Right);
        }
        object CalcDivision(Binary binary)
        {
            Assert(binary.Operator == BinaryOperator.Division);
            return Calc<int>(binary.Left) / Calc<int>(binary.Right);
        }
        object CalcReminder(Binary binary)
        {
            Assert(binary.Operator == BinaryOperator.Remainder);
            return Calc<int>(binary.Left) % Calc<int>(binary.Right);
        }
        object CalcEqual(Binary binary)
        {
            Assert(binary.Operator == BinaryOperator.Equal);
            var a = Calc(binary.Left);
            var b = Calc(binary.Right);
            if (a == null || b == null)
            {
                return (a == null) == (b == null);
            }
            if (a.GetType() != b.GetType())
            {
                return false;
            }
            if (a is int)
            {
                return (int)a == (int)b;
            }
            if (a is bool)
            {
                return (bool)a == (bool)b;
            }
            if (a is IReferenceEquatable)
            {
                return a == b;
            }
            throw MakeError(binary, $"Неверный тип операндов {a} {b}");
        }
        object CalcLess(Binary binary)
        {
            Assert(binary.Operator == BinaryOperator.Less);
            var a = Calc(binary.Left);
            var b = Calc(binary.Right);
            if (a == null && b == null)
            {
                return false;
            }
            if (a is bool && b is bool)
            {
                return !(bool)a && (bool)b;
            }
            if (a is int && b is int)
            {
                return (int)a < (int)b;
            }
            throw MakeError(binary, $"Неверный тип операндов {a} {b}");
        }
        #endregion
        public object VisitParentheses(Parentheses parentheses)
        {
            return Calc(parentheses.Expr);
        }
        public object VisitCall(Call call)
        {
            var value = Calc(call.Function);
            var function = value as ICallable;
            if (function == null)
            {
                throw MakeError(call, $"Вызвали не функцию, а {value}");
            }
            var args = call.Arguments.Select(Calc).ToArray();
            return function.Call(args);
        }
        public object VisitNumber(Number number)
        {
            int value;
            if (int.TryParse(number.Lexeme, NumberStyles.None, NumberFormatInfo.InvariantInfo, out value))
            {
                return value;
            }
            throw MakeError(number, $"Не удалось преобразовать {number.Lexeme} в int");
        }
        public object VisitIdentifier(Identifier identifier)
        {
            object value;
            if (variables.TryGetValue(identifier.Name, out value))
            {
                return value;
            }
            throw MakeError(identifier, $"Неизвестная переменная {identifier.Name}");
        }
        public object VisitMemberAccess(MemberAccess memberAccess)
        {
            throw new NotSupportedException();
        }
        #endregion
        Exception MakeError(IExpression expression, string message)
        {
            return new Exception(LexerUtils.MakeErrorMessage(source, expression.Position, message));
        }
        Exception MakeError(Assignment statement, string message)
        {
            return new Exception(LexerUtils.MakeErrorMessage(source, statement.Expr.Position, message));
        }
    }
}
