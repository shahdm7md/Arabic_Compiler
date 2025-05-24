using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace COMPILE_RPROJECT
{
    internal class Lexer
    {
        private readonly string _code;
        private int _position;
        private int _line;
        private int _column;

        // Define token patterns
        private readonly List<(Regex pattern, TokenType type)> _tokenPatterns = new()
        {
            (new Regex(@"^[ \t]+", RegexOptions.Compiled), TokenType.WS),        
            (new Regex(@"^\r?\n", RegexOptions.Compiled), TokenType.NEWLINE),    
            (new Regex(@"^//[^\n]*", RegexOptions.Compiled), TokenType.COMMENT), 
            (new Regex(@"^#.*?#", RegexOptions.Compiled | RegexOptions.Singleline), TokenType.COMMENT), 
            (new Regex(@"^\d+", RegexOptions.Compiled), TokenType.NUM),
            (new Regex(@"^""(.*?)""", RegexOptions.Compiled), TokenType.STRING),
            (new Regex(@"^(<=|>=|==|!=|<|>)", RegexOptions.Compiled), TokenType.RELATIONALOPERATOR),
            (new Regex(@"^[\+\-\*/]", RegexOptions.Compiled), TokenType.OPERATOR),
            (new Regex(@"^=", RegexOptions.Compiled), TokenType.ASSIGN),
            (new Regex(@"^\(", RegexOptions.Compiled), TokenType.LPAREN),
            (new Regex(@"^\)", RegexOptions.Compiled), TokenType.RPAREN),
            (new Regex(@"^\{", RegexOptions.Compiled), TokenType.LBRACE),
            (new Regex(@"^\}", RegexOptions.Compiled), TokenType.RBRACE),
            (new Regex(@"^;", RegexOptions.Compiled), TokenType.SEMICOLON),
            (new Regex(@"^[\u0621-\u064A_][\u0621-\u064A0-9_]*", RegexOptions.Compiled), TokenType.IDENT) 
        };

        private readonly HashSet<string> _keywords = new() { "إذا", "طالما", "متغير" };

        public Lexer(string code)
        {
            _code = code;
            _position = 0;
            _line = 1;
            _column = 1;
        }

        public List<Token> Tokenize()
        {
            var tokens = new List<Token>();

            while (_position < _code.Length)
            {
                var token = MatchToken();

                if (token != null)
                {
                    if (token.Type != TokenType.WS && token.Type != TokenType.COMMENT && token.Type != TokenType.NEWLINE)
                    {
                        tokens.Add(token);
                    }
                }
                else
                {
                    tokens.Add(new Token(TokenType.ERROR, $"Unexpected character '{_code[_position]}' at line {_line}, column {_column}", _line, _column));
                    _position++;
                    _column++;
                }
            }

            return tokens;
        }

        private Token MatchToken()
        {
            var remainingCode = _code.Substring(_position);

            // First check for keywords before general identifiers
            foreach (var keyword in _keywords)
            {
                if (remainingCode.StartsWith(keyword))
                {
                    UpdatePosition(keyword);
                    return new Token(TokenType.KEYWORD, keyword, _line, _column - keyword.Length);
                }
            }

            foreach (var (pattern, type) in _tokenPatterns)
            {
                var match = pattern.Match(remainingCode);
                if (match.Success)
                {
                    var value = match.Value;
                    var tokenType = type == TokenType.IDENT && _keywords.Contains(value) ? TokenType.KEYWORD : type;

                    UpdatePosition(value);
                    return new Token(tokenType, value, _line, _column - value.Length);
                }
            }

            return null;
        }

        private void UpdatePosition(string value)
        {
            int newLines = value.Count(c => c == '\n');

            if (newLines > 0)
            {
                _line += newLines;
                _column = value.Length - value.LastIndexOf('\n') - 1;
            }
            else
            {
                _column += value.Length;
            }

            _position += value.Length;
        }
    }
}
