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
	sealed class Interpreter : IStatementVisitor, IExpressionVisitor<object>
	{
		readonly string source;
		Dictionary<string, object> variables = new Dictionary<string, object>();
		public Interpreter(string source, IReadOnlyDictionary<string, object> customVariables)
		{
			this.source = source;
			variables = new Dictionary<string, object> {
				{ "true", true },
				{ "false", false },
				{ "null", null },
				{ "dump", new DumpFunction() },
				{ "trace", new TraceFunction() },
				{ "len", new LenFunction() },
			};
			foreach (var kv in customVariables)
			{
				variables[kv.Key] = kv.Value;
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
			foreach (var statement in block.Statements)
			{
				Run(statement);
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
			variables[assignment.Variable] = Calc(assignment.Expr);
		}
		public void VisitAssignmentMas(AssignmentMas assignmentMas)
		{
			object[] newObject = new object[assignmentMas.Expr.Length];
			for (int i = 0; i < newObject.Length; i++)
			{
				newObject[i] = Calc(assignmentMas.Expr[i]);
			}
			variables[assignmentMas.Variable] = newObject;
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
			var left = Calc(binary.Left);
			var right = Calc(binary.Right);
			if (left.GetType() == typeof(object[]) && right.GetType() == typeof(object[]))
			{
				object[] res = new object[((object[])left).Length + ((object[])right).Length];
				Array.Copy(((object[])left), 0, res, 0, ((object[])left).Length);
				Array.Copy(((object[])right), 0, res, ((object[])left).Length, ((object[])right).Length);
				return res;
			}
			else
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
			var left = Calc(binary.Left);
			if (left.GetType() == typeof(object[]))
			{
				object[] res = new object[((object[])left).Length * Calc<int>(binary.Right)];
				int len = Calc<int>(binary.Right);
				for (int i = 0; i < len; i++)
				{
					Array.Copy(((object[])left), 0, res, i * ((object[])left).Length, ((object[])left).Length);
				}
				return res;
			}
			else
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

			return CalcEqualArray((object[])a, (object[])b, binary);
		}
		bool CalcEqualArray(object a, object b, Binary binary)
		{
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
			if (a.GetType() == typeof(object[]) && b.GetType() == typeof(object[]))
			{
				object[] arrayA = (object[])a;
				object[] arrayB = (object[])b;
				for (int i = 0; i < arrayA.Length && i<arrayB.Length; i++)
				{
					if (!CalcEqualArray(arrayA[i], arrayB[i], binary))
						return false;
				}
				if (arrayA != arrayB)
					return false;
				return true;
			}
			return false;
		}
		object CalcLess(Binary binary)
		{
			Assert(binary.Operator == BinaryOperator.Less);
			var a = Calc(binary.Left);
			var b = Calc(binary.Right);

			// < для массивов
			return CalcLessArray(a, b, binary);
		}
		bool CalcLessArray(object a, object b, Binary binary)
		{
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
				return (int)a <= (int)b;
			}
			if (a.GetType() == typeof(object[]) && b.GetType() == typeof(object[]))
			{
				object[] arrayA = (object[])a;
				object[] arrayB = (object[])b;
				int i = 0;
				if (arrayA.Length < arrayB.Length)
				{
					while (i < arrayA.Length)
					{
						if (!CalcLessArray(arrayA[i], arrayB[i], binary))
							return false;
						i++;
					}
					return true;
				}
				else if(arrayA.Length > arrayB.Length)
				{
					return false;
				}
				else
				{
					while (i < arrayB.Length)
					{
						if (!CalcLessArray(arrayA[i], arrayB[i], binary))
							return false;
						i++;
					}
					return CalcLessArray(arrayA[i-1], arrayB[i-1], binary);
				}
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
		public object VisitArrayExpr(ArrayExpr arrayExpr)
		{
			object[] newObject = new object[arrayExpr.Expr.Count];
			for (int i = 0; i < newObject.Length; i++)
			{
				newObject[i] = Calc(arrayExpr.Expr[i]);
			}
			return newObject;
		}
		public object VisitNumber(Number number)
		{
			int value;
			if (int.TryParse(number.Lexeme, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out value))
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

		public object VisitArrayIndex(ArrayIndex arrayIndex)
		{
			object[] objArray = Calc<object[]>(arrayIndex.variable);

			int left = (int)Calc(arrayIndex.l);
			int right;
			if (arrayIndex.r != null)
				right = (int)Calc(arrayIndex.r);
			else
				return ((objArray)[left]);

			object[] resArray = new object[right - left];
			for (int i = 0; i < resArray.Length; i++)
			{
				resArray[i] = ((objArray)[i + left]);
			}
			return resArray;
		}
	}
}
