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
                Console.Write("'");
                if (car.isPair() && p)
                    Console.Write("(");
                car.print(n);
               // cdr.print(n - 2);
            }
            else if (FirstInQuoted(n)) // ' is the first expr in a quoted expression
            {
                Console.Write("'");
                if (car.isPair() && p)
                    Console.Write("(");
                car.print(n);
               // cdr.print(n + 1); // n - 1
            }
            else if (FirstAfterDefine(n)) // first exp after special type: if, lambda, define
            {
                Console.Write("'");
                if (car.isPair() && p)
                    Console.Write("(");
                car.print(n - 2); // n - 1
            }
            else if (InitExpSpecial(n)) // beginning of an exp within a special type.
            {
                for (int k = 0; k < n / 4; k++)
                    Console.Write("    ");

                Console.Write("'");
                if (car.isPair() && p)
                    Console.Write("(");
                car.print(n - 1);
            }
            else
            {
                Console.Write("'");
                if (car.isPair() && p)
                    Console.Write("(");
                car.print(n - 1);
                //cdr.print(n - 1);
            }
        }
    }
}

