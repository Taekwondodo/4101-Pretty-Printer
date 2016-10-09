// Special -- Parse tree node strategy for printing special forms

using System;

namespace Tree
{
    // There are several different approaches for how to implement the Special
    // hierarchy.  We'll discuss some of them in class.  The easiest solution
    // is to not add any fields and to use empty constructors.

    abstract public class Special
    {
        public abstract void print(Node t, int n, bool p);

        // Methods to determine printing style

        public bool WithinQuoted(int n)
        { return n % 4 == 3; }
        public bool FirstInQuoted(int n)
        { return n % 4 == 2; }    
        public bool FirstAfterDefine(int n)
        { return n % 4 == 1; }
        public bool InitExpSpecial(int n)
        { return (n > 0 && n % 4 == 0); }

        public void PrintBeginLetCond(Node t, int n)
        {
            Node cdr = t.getCdr();
            Ident car = (Ident)t.getCdr();
            string identName = car.getName();

            if (WithinQuoted(n))
            {
                Console.Write(identName + " ");
                cdr.print(n);
            }
            else if (FirstInQuoted(n))
            {
                Console.Write(identName + " ");
                cdr.print(n + 2); // n
            }
            else if (FirstAfterDefine(n))
            {
                Console.Write(identName + " ");
                cdr.print(n - 2); // n - 1 We want the whole thing printed on one line, so we treat it like a quoted list.
                Console.WriteLine();
            }
            else if (InitExpSpecial(n)) // Correct syntax won't bring us here
            {
                for (int k = 0; k < n / 4; k++)
                    Console.Write("    ");

                Console.Write(identName + " ");
                cdr.print(n * -1);
            }
            else
            {
                Console.Write(identName + " ");
                Console.WriteLine();
                cdr.print(n + 4);
            }
        }

        public void PrintIfLamDef(Node t, int n)
        {
            Node cdr = t.getCdr();
            Ident car = (Ident)t.getCdr();
            string identName = car.getName();

            if (WithinQuoted(n))
            {
                Console.Write(identName + " ");
                cdr.print(n);
            }
            else if (FirstInQuoted(n))
            {
                Console.Write(identName + " ");
                cdr.print(n + 2); // n
            }
            else if (FirstAfterDefine(n))
            {
                Console.Write(identName + " ");
                cdr.print(n - 2); // We want the whole thing printed on one line, so we treat it like a quoted list.
                Console.WriteLine();
            }
            else if (InitExpSpecial(n)) // Correct syntax shouldn't bring us here
            {
                for (int k = 0; k < n / 4; k++)
                    Console.Write("    ");

                Console.Write(identName + " ");
                cdr.print(n * -1);
            }
            else
            {
                Console.Write(identName + " ");
                cdr.print(n + 5); // n + 4 + 1
            }
        }
    }
}

