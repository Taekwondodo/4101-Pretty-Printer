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

            if (n % 4 == 3) // Within a quoted expression
            {
                for (int k = 0; k < (n + 1) / 4; k++)
                    Console.Write("    ");

                car.print(n);
                cdr.print(n);
            }
            else if (n % 4 == 1) // first exp after special type: if, lambda, define
            {
                if (car.isPair())
                    Console.Write("(");
                car.print(0);
                Console.WriteLine("");
                cdr.print(n - 1);
            }
            else if (n >= 0) // beginning of an exp in a special type (excluding set!)
            {
                for (int k = 0; k < n / 4; k++)
                    Console.Write("    ");

                if (car.isPair())
                    Console.Write("(");
                car.print(n * -1);
                cdr.print(n);
            }
            else 
            {
                car.print(n);
                cdr.print(n);
            }
        }
    }
}


