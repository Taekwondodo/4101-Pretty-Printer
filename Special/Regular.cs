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
                if (car.isPair() && p)
                    Console.Write("(");
                car.print(n);
                cdr.print(n);
            }
            else if (FirstInQuoted(n)) // car is the first expr in a quoted expression
            {
                if (car.isPair() && p)
                    Console.Write("(");

                car.print(n + 1, p); // n - 1
                // We don't print cdr since it is nil, and the binary has an extra nilNode due to how we have this setup.
            }
            else if (FirstAfterDefine(n)) // first exp after special type: if, lambda, define
            {
                if (car.isPair() && p)
                    Console.Write("(");
                car.print(-1); // treat as a quoted expr. We want the next exp printed out on one line
                Console.WriteLine();
                cdr.print(n - 1);
            }
            else if (InitExpSpecial(n)) // beginning of an exp thats indented in a special type
            {
                for (int k = 0; k < n / 4; k++)
                    Console.Write("    ");

                if (car.isPair() && p)
                    Console.Write("(");
                car.print(n * -1);
                Console.WriteLine();
                cdr.print(n);
            }
            else // regular list, not indented
            {
                if (car.isPair() && p)
                    Console.Write("(");
                car.print(n);
                cdr.print(n);
            }
        }
    }
}


