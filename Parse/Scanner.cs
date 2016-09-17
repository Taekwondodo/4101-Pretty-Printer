// Scanner -- The lexical analyzer for the Scheme printer and interpreter

using System;
using System.IO;
using Tokens;

namespace Parse
{
    
    public class Scanner
    {
        private TextReader In;

        // maximum length of strings and identifier
        private const int BUFSIZE = 1000;
        private char[] buf = new char[BUFSIZE];

        public Scanner(TextReader i) { In = i; }
        
  
        // TODO: Add any other methods you need

        public Token getNextToken()
        {
            int ch;

            try
            {
                // It would be more efficient if we'd maintain our own
                // input buffer and read characters out of that
                // buffer, but reading individual characters from the
                // input stream is easier.
                ch = In.Read();
   
                // TODO: skip white space and comments
                
                while (ch == ' ' 
                    || ch == ';' 
                    || ch == 13 
                    || ch == 10) // Also skip carriage returns(13) and line feeds(10) since that is how Windows does newlines
                {
                    if (ch == ';')
                         In.ReadLine();
                    ch = In.Read();
                }

                if (ch == -1)
                    return null;

                // Special characters
                else if (ch == '\'')
                    return new Token(TokenType.QUOTE);
                else if (ch == '(')
                    return new Token(TokenType.LPAREN);
                else if (ch == ')')
                    return new Token(TokenType.RPAREN);
                else if (ch == '.')
                    // We ignore the special identifier `...'.
                    return new Token(TokenType.DOT);

                // Boolean constants
                else if (ch == '#')
                {
                    ch = In.Read();

                    if (ch == 't')
                        return new Token(TokenType.TRUE);
                    else if (ch == 'f')
                        return new Token(TokenType.FALSE);
                    else if (ch == -1)
                    {
                        Console.Error.WriteLine("Unexpected EOF following #");
                        return null;
                    }
                    else
                    {
                        Console.Error.WriteLine("Illegal character '" +
                                                (char)ch + "' following #");
                        return getNextToken();
                    }
                }

                // String constants
                else if (ch == '"')
                {
                    int x;
                    ch = In.Read();

                    for (x = 0; ch != '"'; x++)
                    {
                        buf[x] = (char)(ch);
                        ch = In.Read();
                    }
                    // TODO: scan a string into the buffer variable buf
                    return new StringToken(new String(buf, 0, x + 1));

                }


                // Integer constants
                else if (ch >= '0' && ch <= '9')
                {
                    int i = 0;
                    // TODO: scan the number and convert it to an integer
                    do
                    {
                        i = i * 10 + (ch - '0'); // Adding another digit to the number increases the value of the digits by a factor of 10 each time
                        ch = In.Read();
                    } while (ch >= '0' && ch <= '9');

                    // make sure that the character following the integer
                    // is not removed from the input stream
                    return new IntToken(i);
                }

                // Identifiers
                else if ((ch >= 'A' && ch <= 'Z')
                       ||(ch >= '$' && ch <= '&')
                       ||(ch >= '<' && ch <= '@')
                       ||(ch >= '*' && ch <= '+')
                       ||(ch >= '-' && ch <= '/')
                       ||(ch >= '^' && ch <= '_')
                       || ch == '!'
                       || ch == ':'
                       || ch == '~'
                       // or ch is some other valid first character
                         // for an identifier
                         )
                {
                    // TODO: scan an identifier into the buffer

                    // make sure that the character following the integer
                    // is not removed from the input stream

                    int x;

                    for (x = 0; ch != ' '; x++)
                    {
                        buf[x] = (char)(ch);
                        ch = In.Read();
                    }

                    return new IdentToken(new String(buf, 0, x + 1));
                }

                // Illegal character
                else
                {
                    Console.Error.WriteLine("Illegal input character '"
                                            + (char)ch + '\'');
                    return getNextToken();
                }
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("IOException: " + e.Message);
                return null;
            }
        }
    }

}

