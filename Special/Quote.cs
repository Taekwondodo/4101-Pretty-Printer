// Quote -- Parse tree node strategy for printing the special form quote

using System;

namespace Tree
{
    public class Quote : Special
    {
        // TODO: Add any fields needed.
  
        // TODO: Add an appropriate constructor.
	public Quote() { }

        public override void print(Node t, int n, bool p)
        {
            Node car, cdr;
            car = t.getCar();
            cdr = t.getCdr();

            if (WithinQuoted(n)) // Within a quoted expression
            {
                Console.Write("' ");
                cdr.print(n);
            }
            else if (FirstInQuoted(n)) // ' is the first expr in a quoted expression
            {
                Console.Write("'");
                cdr.print(n + 2); // n
            }
            else if (FirstAfterDefine(n)) // first exp after special type: if, lambda, define
            {
                Console.Write("'");
                cdr.print(n - 3); // n - 2
            }
            else if (InitExpSpecial(n)) // beginning of an exp within a special type.
            {
                for (int k = 0; k < n / 4; k++)
                    Console.Write("    ");

                Console.Write("'");
                cdr.print(n - 2);
            }
            else
            {
                Console.Write("'");
                cdr.print(n - 2);
            }
        }
    }
}

