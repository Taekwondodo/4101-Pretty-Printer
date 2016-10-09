// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree
{
    public class Define : Special
    {
        // TODO: Add any fields needed.

        // TODO: Add an appropriate constructor.
	public Define() { }

        public override void print(Node t, int n, bool p)
        {
            // Determine either variable or function definition syntax by looking at the cadr
            if (!WithinQuoted(n))
            {
                Node pita = t.getCdr().getCar();
                // Treat the rest of the list as quoted if variable syntax
                if (!pita.isPair())
                    n = -1;
            }    
            PrintIfLamDef(t, n);
        }
    }
}


