// Parser -- the parser for the Scheme printer and interpreter
//
// Defines
//
//   class Parser;
//
// Parses the language
//
//   exp  ->  ( rest
//         |  #f
//         |  #t
//         |  ' exp
//         |  integer_constant
//         |  string_constant
//         |  identifier
//    rest -> )
//         |  exp+ [. exp] )
//
// and builds a parse tree.  Lists of the form (rest) are further
// `parsed' into regular lists and special forms in the constructor
// for the parse tree node class Cons.  See Cons.parseList() for
// more information.
//
// The parser is implemented as an LL(0) recursive descent parser.
// I.e., parseExp() expects that the first token of an exp has not
// been read yet.  If parseRest() reads the first token of an exp
// before calling parseExp(), that token must be put back so that
// it can be reread by parseExp() or an alternative version of
// parseExp() must be called.
//
// If EOF is reached (i.e., if the scanner returns a NULL) token,
// the parser returns a NULL tree.  In case of a parse error, the
// parser discards the offending token (which probably was a DOT
// or an RPAREN) and attempts to continue parsing with the next token.

using System;
using Tokens;
using Tree;

namespace Parse
{
    public class Parser {
	
        private Scanner scanner;
        // He wants only 1 instance of each of the following type of Node, so I made them fields of this class
        // since it is only created once
        private BoolLit trueNode = new BoolLit(true);
        private BoolLit falseNode = new BoolLit(false);
        private Nil nilNode = new Nil();

        public Parser(Scanner s) { scanner = s; }
  
        public Node parseExp(Token tok = null) // Optional parameter used when called within parseRest() incase it reads the first token of an exp
        {
            if (tok == null)
                tok = scanner.getNextToken(); 

            if (tok != null)
            {
                TokenType tt = tok.getType();
                if (tt == TokenType.LPAREN)
                    return parseRest();
                else if (tt == TokenType.FALSE)
                    return falseNode;
                else if (tt == TokenType.TRUE)
                    return trueNode;
                else if (tt == TokenType.QUOTE)
                {
                    Quote k = new Quote();
                    Cons temp = new Cons(parseExp(), nilNode);
                    if (temp.getCar() == null)
                    {
                        Console.Error.WriteLine("Valid expression must follow a quote");
                        return parseExp();
                    }
                    temp.SetForm(k);
                    return temp;
                }
    
                else if (tt == TokenType.INT)
                    return new IntLit(tok.getIntVal());
                else if (tt == TokenType.STRING)
                    return new StringLit(tok.getStringVal());
                else if (tt == TokenType.IDENT)
                    return new Ident(tok.getName());
                
                // DOT and RPAREN shouldn't be read within parseExp(), I think
                else if (tt == TokenType.DOT)
                {
                    Console.Error.WriteLine("Illegal DOT Grammar");
                    return parseExp();
                }
                else if (tt == TokenType.RPAREN)
                {
                    Console.Error.WriteLine("Illegal RPAREN Grammar");
                    return parseExp();
                }
            }

            return null;       
        }
  
        protected Node parseRest()
        {
            Token tok = scanner.getNextToken();

            if (tok != null)
            {
                TokenType tt = tok.getType();

                if (tt == TokenType.RPAREN)
                    return nilNode;
                else if (tt == TokenType.DOT)
                    return parseExp();
                else
                    return new Cons(parseExp(tok), parseRest());     
            }
            else
                return null;
        }

        // TODO: Add any additional methods you might need.
    }
}

