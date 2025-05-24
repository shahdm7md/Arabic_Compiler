using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPILE_RPROJECT
{

	public enum TokenType
	{
		NUM,        // For numbers
		IDENT,      // For identifiers (variables, functions)
		KEYWORD,    // For reserved keywords (like إذا, طالما)
		OPERATOR,
		RELATIONALOPERATOR,
		LPAREN,     // Left parenthesis '('
		RPAREN,     // Right parenthesis ')'
		LBRACE,     // Left brace '{'
		RBRACE,     // Right brace '}'
		STRING,     // For string literals
		DOT,        // For dot '.'
		ERROR,      // For invalid or unrecognized characters
		COMMENT,    // For comments (single-line and multi-line)
		SEMICOLON,  // For semicolons ';'
		ASSIGN,     // For assignment operator '='
		WS,          // For whitespace (spaces, tabs, etc.)
		NEWLINE
	}

	public class Token
	{
		public TokenType Type { get; }
		public string Value { get; }
		public int Line { get; }
		public int Column { get; }

		public Token(TokenType type, string value, int line, int column)
		{
			Type = type;
			Value = value;
			Line = line;
			Column = column;
		}


		public override string ToString()
		{
			return $"{Type}: {Value} (Line: {Line}, Column: {Column})";
		}
	}
}

