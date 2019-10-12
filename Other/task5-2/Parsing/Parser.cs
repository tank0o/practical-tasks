using Lab5.Ast;
using Lab5.Ast.Expressions;
using Lab5.Ast.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Lab5.Parsing
{
	sealed class Parser
	{
		readonly IReadOnlyList<Token> tokens;
		readonly string source;
		int position = 0;
		Token CurrentToken => tokens[position];
		Parser(IReadOnlyList<Token> tokens, string source)
		{
			this.tokens = tokens;
			this.source = source;
		}
		#region stuff
#if DEBUG
		string[] DebugCurrentPosition => LexerUtils.FormatLines(source, CurrentToken.Position,
			inlinePointer: true,
			pointer: " <|> "
			).ToArray();
		string DebugCurrentLine => string.Join("", LexerUtils.FormatLines(source, CurrentToken.Position,
			linesAround: 0,
			inlinePointer: true,
			pointer: " <|> "
			).ToArray());
#endif
		static bool IsNotWhitespace(Token t)
		{
			switch (t.Type)
			{
				case TokenType.Whitespaces:
				case TokenType.SingleLineComment:
				case TokenType.MultiLineComment:
					return false;
			}
			return true;
		}
		void ExpectEof()
		{
			if (!IsType(TokenType.EnfOfFile))
			{
				throw MakeError($"Не допарсили до конца, остался {CurrentToken}");
			}
		}
		void ReadNextToken()
		{
			position += 1;
		}
		void Reset()
		{
			position = 0;
		}
		Exception MakeError(string message)
		{
			return new Exception(LexerUtils.MakeErrorMessage(source, CurrentToken.Position, message));
		}
		bool SkipIf(string s)
		{
			if (CurrentIs(s))
			{
				ReadNextToken();
				return true;
			}
			return false;
		}
		bool CurrentIs(string s) => string.Equals(CurrentToken.Lexeme, s, StringComparison.Ordinal);
		bool IsType(TokenType type) => CurrentToken.Type == type;
		void Expect(string s)
		{
			if (!SkipIf(s))
			{
				throw MakeError($"Ожидали \"{s}\", получили {CurrentToken}");
			}
		}
		#endregion
		public static ProgramNode Parse(string source)
		{
			var eof = new Token(TokenType.EnfOfFile, "", source.Length);
			var tokens = Lexer.GetTokens(source).Concat(new[] { eof }).Where(IsNotWhitespace).ToList();
			var parser = new Parser(tokens, source);
			return parser.ParseProgram();
		}
		ProgramNode ParseProgram()
		{
			Reset();
			var statements = new List<IStatement>();
			while (!IsType(TokenType.EnfOfFile))
			{
				statements.Add(ParseStatement());
			}
			var result = new ProgramNode(statements);
			ExpectEof();
			return result;
		}
		Block ParseBlock()
		{
			Expect("{");
			var statements = new List<IStatement>();
			while (!SkipIf("}"))
			{
				statements.Add(ParseStatement());
			}
			return new Block(statements);
		}
		IStatement ParseStatement()
		{

			if (SkipIf("if"))
			{
				Expect("(");
				var condition = ParseExpression();
				Expect(")");
				var block = ParseBlock();
				return new IfStatement(condition, block);
			}
			if (SkipIf("while"))
			{
				Expect("(");
				var condition = ParseExpression();
				Expect(")");
				var block = ParseBlock();
				return new WhileStatement(condition, block);
			}
			var expression = ParseExpression();

			if (SkipIf("="))
			{
				var identifier = expression as Identifier;
				if (identifier == null)
				{
					throw MakeError("Присваивание не в переменную");
				}
				var restAssigmentExpression = ParseExpression();
				Expect(";");
				return new Assignment(identifier.Name, restAssigmentExpression);
			}
			else
			{
				Expect(";");
				return new ExpressionStatement(expression);
			}
		}
		string ParseIdentifier()
		{
			if (!IsType(TokenType.Identifier))
			{
				throw MakeError($"Ожидали идентификатор, получили {CurrentToken}");
			}
			var lexeme = CurrentToken.Lexeme;
			ReadNextToken();
			return lexeme;
		}
		#region expressions
		IExpression ParseExpression()
		{
			return ParseEqualityExpression();
		}

		IExpression ParseEqualityExpression()
		{
			var left = ParseRelationalExpression();
			while (true)
			{
				var pos = CurrentToken.Position;
				if (SkipIf("=="))
				{
					var right = ParseRelationalExpression();
					left = new Binary(pos, left, BinaryOperator.Equal, right);
				}
				else
				{
					break;
				}
			}
			return left;
		}
		IExpression ParseRelationalExpression()
		{
			var left = ParseAdditiveExpression();
			while (true)
			{
				var pos = CurrentToken.Position;
				if (SkipIf("<"))
				{
					var right = ParseAdditiveExpression();
					left = new Binary(pos, left, BinaryOperator.Less, right);
				}
				else
				{
					break;
				}
			}
			return left;
		}
		IExpression ParseAdditiveExpression()
		{
			var left = ParseMultiplicativeExpression();
			while (true)
			{
				var pos = CurrentToken.Position;
				if (SkipIf("+"))
				{
					var right = ParseMultiplicativeExpression();
					left = new Binary(pos, left, BinaryOperator.Addition, right);
				}
				else if (SkipIf("-"))
				{
					var right = ParseMultiplicativeExpression();
					left = new Binary(pos, left, BinaryOperator.Subtraction, right);
				}
				else
				{
					break;
				}
			}
			return left;
		}
		IExpression ParseMultiplicativeExpression()
		{
			var left = ParsePrimary();
			while (true)
			{
				var pos = CurrentToken.Position;
				if (SkipIf("*"))
				{
					var right = ParsePrimary();
					left = new Binary(pos, left, BinaryOperator.Multiplication, right);
				}
				else if (SkipIf("/"))
				{
					var right = ParsePrimary();
					left = new Binary(pos, left, BinaryOperator.Division, right);
				}
				else if (SkipIf("%"))
				{
					var right = ParsePrimary();
					left = new Binary(pos, left, BinaryOperator.Remainder, right);
				}
				else
				{
					break;
				}
			}
			return left;
		}
		IExpression ParsePrimary()
		{
			var expression = ParsePrimitive();
			while (true)
			{
				int pos = CurrentToken.Position;
				if (SkipIf("("))
				{
					var arguments = new List<IExpression>();
					if (!CurrentIs(")"))
					{
						arguments.Add(ParseExpression());
						while (SkipIf(","))
						{
							arguments.Add(ParseExpression());
						}
					}
					Expect(")");
					expression = new Call(pos, expression, arguments);
				}
				else if (SkipIf("."))
				{
					var member = ParseIdentifier();
					expression = new MemberAccess(pos, expression, member);
				}
				else if (SkipIf("["))
				{
					IExpression parant = expression;
					IExpression expressionleft;
					IExpression expressionRight;

					expressionleft = null;
					expressionRight = null;

					expressionleft = ParseExpression();
					if (SkipIf(":"))
					{
						expressionRight = ParseExpression();

						expression = new ArrayIndex(expression, expressionleft, expressionRight);
					}
					else
						expression = new ArrayIndex(expression, expressionleft);
					Expect("]");
				}
				else
				{
					break;
				}
			}
			return expression;
		}
		IExpression ParseArrayExpr()
		{
			List<IExpression> newObj = new List<IExpression>();
			if (SkipIf("]"))
				return new ArrayExpr(newObj);
			do
			{
				newObj.Add(ParseExpression());
			} while (SkipIf(","));
			Expect("]");
			return new ArrayExpr(newObj);

		}
		IExpression ParsePrimitive()
		{
			var pos = CurrentToken.Position;
			if (SkipIf("("))
			{
				var expression = new Parentheses(pos, ParseExpression());
				Expect(")");
				return expression;
			}
			if (SkipIf("["))
			{
				return ParseArrayExpr();
			}
			if (IsType(TokenType.NumberLiteral))
			{
				var lexeme = CurrentToken.Lexeme;
				ReadNextToken();
				return new Number(pos, lexeme);
			}
			else if (IsType(TokenType.Identifier))
			{
				var lexeme = CurrentToken.Lexeme;
				ReadNextToken();
				return new Identifier(pos, lexeme);
			}
			throw MakeError($"Ожидали идентификатор, число или скобку, получили {CurrentToken}");
		}
		#endregion
	}
}
