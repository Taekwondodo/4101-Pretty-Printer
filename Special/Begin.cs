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
            Node cdr = t.getCdr();

            if (n == -1) // Is this a literal?
            {
                Console.Write("Begin");
                cdr.print(n);
            }
            else
            {
                Console.WriteLine("Begin");

                n *= -1;
                n += 4;
                cdr.print(n + 4);
            }

            
        }
    }
}

