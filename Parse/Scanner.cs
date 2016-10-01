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

        public int UpperCase(int ch) 
        {
            if (ch >= 'a' && ch <= 'z')
                return ch - 32;
            return ch;
        }

        public bool IsValidIdent(int ch)
        {
            if ((ch >= 'A' && ch <= 'Z')
             || (ch >= '$' && ch <= '&')
             || (ch >= '<' && ch <= '@')
             || (ch >= '*' && ch <= '+')
             || (ch >= '-' && ch <= '/')
             || (ch >= '^' && ch <= '_')
             || (ch >= '0' && ch <= '9')
             || ch == '!'
             || ch == ':'
             || ch == '~'
            )
                return true;
            return false;
        }

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
                ch = UpperCase(ch);

                // TODO: skip white space and comments

                while (ch == ' ' 
                    || ch == ';' 
                    || ch == 13
                    || ch == 10 
                    ) // Also skip carriage returns(13) and line feeds(10) since that is how Windows does newlines
                {
                    if (ch == ';')
                         In.ReadLine();
                    ch = In.Read();
                    ch = UpperCase(ch);
                }

                if (ch == -1)
                    return null;

                // Special characters
                if (ch == '\'')
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
                    ch = UpperCase(ch);

                    if (ch == 'T')
                        return new Token(TokenType.TRUE);
                    else if (ch == 'F')
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

                        if (ch == -1)
                        {
                            Console.Error.WriteLine("Unexpected EOF following string" + new string(buf, 0, x) + "'");
                            return null;
                        }
                    }
                    // TODO: scan a string into the buffer variable buf
                    return new StringToken(new String(buf, 0, x));

                }


                // Integer constants
                else if (ch >= '0' && ch <= '9')
                {

                    // make sure that the character following the integer
                    // is not removed from the input stream

                    int i = ch - '0';
                    ch = In.Peek(); // Using peek helps us to not remove the character after the int 

                    // TODO: scan the number and convert it to an integer
                    while(ch >= '0' && ch <= '9')
                    {
                        i = i * 10 + (ch - '0'); // Adding another digit to a number increases the 
                                              // value of the original by a factor of 10 each time
                        In.Read(); // Advance the stream position
                        ch = In.Peek();
                    } 

                    // Make sure it is actually an int constant
                    if (ch != ' '
                     && ch != 13
                     && ch != ')'
                     && ch != ';') // Only valid characters directly after an int
                    {
                        In.Read(); //Advance stream position
                        Console.Error.WriteLine("Invalid character '" + (char)ch + "' following integer constant '" + i + "'");
                        return getNextToken();
                    }
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

                    buf[0] = (char)ch;
                    ch = In.Peek();
                    ch = UpperCase(ch);

                    int x;
                    for (x = 1; IsValidIdent(ch); x++)
                    {
                        buf[x] = (char)ch;

                        In.Read();
                        ch = In.Peek();
                        ch = UpperCase(ch);
                    }

                    // Make sure it is actually an identifier
                    if (ch != ' '
                     && ch != 13
                     && ch != ')'
                     && ch != ';') // Only valid characters directly after an int
                    {
                        In.Read(); // Advance stream position
                        Console.Error.WriteLine("Invalid character '" + (char)ch + "'" + " after identifier '" + new string(buf, 0, x) + "'");
                        return getNextToken();
                    }

                    return new IdentToken(new String(buf, 0, x));
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

