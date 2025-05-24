using System;
using System.Collections.Generic;

namespace COMPILE_RPROJECT
{
    internal class Parser
    {
        private readonly Queue<Token> _tokens;  
        private Token CurrentToken => _tokens.Count > 0 ? _tokens.Peek() : null; 

        public Parser(List<Token> tokens)
        {
            if (tokens == null || tokens.Count == 0)
            {
                throw new ArgumentException("Token list cannot be null or empty.", nameof(tokens));
            }

            _tokens = new Queue<Token>(tokens); 
        }

        public void Parse()
        {
            try
            {
                ParseProgram();
                Console.WriteLine("Code is syntactically valid.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Syntax Error: {ex.Message}");
            }
        }

        private void ParseProgram()
        {
            ParseStatementList();
            if (CurrentToken != null)
            {
                throw new Exception($"Unexpected token '{CurrentToken.Value}' at line {CurrentToken.Line}, column {CurrentToken.Column}. Unexpected content after program statement.");
            }
        }

        private void ParseStatementList()
        {
            while (CurrentToken != null && IsStatementStart(CurrentToken))
            {
                ParseStatement();
            }
        }

        private void ParseStatement()
        {
            if (MatchKeyword("متغير"))
            {
                ParseDeclaration();
            }
            else if (MatchKeyword("إذا"))
            {
                ParseIfStatement();
            }
            else if (MatchKeyword("طالما"))
            {
                ParseWhileStatement();
            }
            else if (CurrentToken.Type == TokenType.IDENT)
            {
                ParseAssignment();
            }
            else
            {
                throw new Exception($"Unexpected token '{CurrentToken.Value}' at line {CurrentToken.Line}, column {CurrentToken.Column}");
            }
        }

        private void ParseDeclaration()
        {
            Match(TokenType.KEYWORD, "متغير");
            Match(TokenType.IDENT);
            Match(TokenType.ASSIGN);
            ParseExpression();
            Match(TokenType.SEMICOLON);
        }

        private void ParseAssignment()
        {
            Match(TokenType.IDENT);
            Match(TokenType.ASSIGN);
            ParseExpression();
            Match(TokenType.SEMICOLON);
        }

        private void ParseIfStatement()
        {
            Match(TokenType.KEYWORD, "إذا");
            Match(TokenType.LPAREN);
            ParseCondition();
            Match(TokenType.RPAREN);
            Match(TokenType.LBRACE);
            ParseStatementList();
            Match(TokenType.RBRACE);
        }

        private void ParseWhileStatement()
        {
            Match(TokenType.KEYWORD, "طالما");
            Match(TokenType.LPAREN);
            ParseCondition();
            Match(TokenType.RPAREN);
            Match(TokenType.LBRACE);
            ParseStatementList();
            Match(TokenType.RBRACE);
        }

        private void ParseCondition()
        {
            ParseExpression(); // Parse the first operand
            if (CurrentToken == null || CurrentToken.Type != TokenType.RELATIONALOPERATOR)
            {
                throw new Exception($"Expected operator in condition but found {CurrentToken?.Type ?? TokenType.ERROR} at line {CurrentToken?.Line}, column {CurrentToken?.Column}");
            }
            Match(TokenType.RELATIONALOPERATOR); // Match the operator
            ParseExpression(); // Parse the second operand
        }

        private void ParseExpression()
        {
            ParseTerm();
            while (CurrentToken != null && (CurrentToken.Type == TokenType.OPERATOR))
            {
                Token op = CurrentToken;
                _tokens.Dequeue();  // Dequeue past the operator
                ParseTerm();
            }
        }

        private void ParseTerm()
        {
            if (CurrentToken.Type == TokenType.NUM || CurrentToken.Type == TokenType.IDENT || CurrentToken.Type == TokenType.STRING)
            {
                _tokens.Dequeue();  // Dequeue the term
            }
            else if (CurrentToken.Type == TokenType.LPAREN)
            {
                _tokens.Dequeue();  // Dequeue the opening parenthesis
                ParseExpression();
                Match(TokenType.RPAREN);
            }
            else
            {
                throw new Exception($"Expected IDENT, NUM, STRING, or LPAREN but found {CurrentToken.Type} at line {CurrentToken.Line}, column {CurrentToken.Column}");
            }
        }

        private void Match(TokenType expectedType, string expectedValue = null)
        {
            if (CurrentToken == null)
            {
                throw new Exception($"Unexpected end of input. Expected {expectedType}");
            }

            if (CurrentToken.Type != expectedType || (expectedValue != null && CurrentToken.Value != expectedValue))
            {
                throw new Exception($"Expected {expectedType} ({expectedValue}), but found {CurrentToken.Type} ({CurrentToken.Value}) at line {CurrentToken.Line}, column {CurrentToken.Column}");
            }

            _tokens.Dequeue();  // Proceed to the next token
        }

        private bool MatchKeyword(string keyword)
        {
            if (CurrentToken?.Type == TokenType.KEYWORD && CurrentToken.Value == keyword)
            {
                 // Proceed to the next token
                return true;
            }
            return false;
        }

        private bool IsStatementStart(Token token)
        {
            return token.Type == TokenType.KEYWORD || token.Type == TokenType.IDENT;
        }
        private bool IsStatementStart(Token toke, Token fromke)
        {
            return false;
        }
    }
}
