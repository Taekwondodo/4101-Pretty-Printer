// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree
{
    public class Regular : Special
    {
        // TODO: Add any fields needed.
    
        // TODO: Add an appropriate constructor.
        public Regular() { }

        public override void print(Node t, int n, bool p)
        {
            Node car, cdr;
            car = t.getCar();
            cdr = t.getCdr();

            if (WithinQuoted(n)) // Within a quoted expression
            {
                if (car.isPair())
                    Console.Write("(");
                car.print(n);
                cdr.print(n);
            }
            else if (FirstInQuoted(n)) // car is the first expr in a quoted expression
            {
                if (car.isPair())
                    Console.Write("(");
                // Quote only affects the first expr after it, so 'n - 1' is only passed to the car
                car.print(n + 1); // n - 1
                cdr.print(n + 2); // n
            }
            else if (FirstAfterDefine(n)) // first exp after special type: if, lambda, define
            {
                if (car.isPair())
                    Console.Write("(");
                car.print(n - 2); // resolves to n - 1. We want the next exp printed out on one line
                Console.WriteLine();
                cdr.print(n - 1);
            }
            else if (InitExpSpecial(n)) // beginning of an exp. If n > 0 then its indented within a special type.
            {
                for (int k = 0; k < n / 4; k++)
                    Console.Write("    ");

                if (car.isPair())
                    Console.Write("(");
                car.print(n * -1);
                Console.WriteLine();
                cdr.print(n);
            }
            else // middle or end of an expr
            {
                if (car.isPair())
                    Console.Write("(");
                car.print(n);
                cdr.print(n);
            }
        }
    }
}


