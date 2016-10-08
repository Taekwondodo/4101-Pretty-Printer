// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree
{
    public class Begin : Special
    {
        // TODO: Add any fields needed.
 
        // TODO: Add an appropriate constructor.
	public Begin() { }

        public override void print(Node t, int n, bool p)
        {
             cdr = t.getCdr();

            if (WithinQuoted(n)) // Is this a literal?
            {
                Console.Write("Begin ");
                cdr.print(n);
            }
            else if (FirstInQuoted(n))
            {
                Console.Write("Begin ");
                cdr.print(n + 2);
            }
            {
                Console.WriteLine("Begin");

                n *= -1;
                n += 4;
                cdr.print(n + 4);
            }

            
        }
    }
}

