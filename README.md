# Arabic Compiler (C#)

This project is a basic compiler built with C# that supports Arabic syntax. It includes:

- A **Lexer** to tokenize Arabic code with support for identifiers, keywords, numbers, strings, and operators.
- A **Parser** that performs syntax validation based on basic constructs like declarations, assignments, conditionals (`إذا`), and loops (`طالما`).

## Features

- Arabic keyword recognition (`متغير`, `إذا`, `طالما`)
- Syntax error reporting with line and column numbers
- Modular code (Lexer, Parser, Token classes)

## Getting Started

1. Open the solution in Visual Studio.
2. Run the project.
3. Input Arabic-based code in the provided form to analyze and parse it.

## Project Structure

- `Lexer.cs`: Tokenizer logic
- `Parser.cs`: Syntax checker
- `Token.cs`: Token model and types
- `Form1.cs`: GUI 
- `Program.cs`: Entry point

## License

This project is for educational use. Feel free to modify and extend.
