// Nil -- Parse tree node class for representing the empty list

using System;

namespace Tree
{
    public class Nil : Node
    {
        public Nil() { }
  
        public override void print(int n)
        {
            print(n, false);
        }

        public override bool isNull()
        {
            return true;
        }

        public override void print(int n, bool p) {
            // There got to be a more efficient way to print n spaces.

            if (n == 0 || n % 4 == 3) // End of a regular list or quoted list
                Console.Write("\b) ");
            else                      // End of a special type
            {
                Console.WriteLine("");
                for (int k = 0; k < (n - 4) / 4; k++)
                    Console.Write("    ");
                Console.WriteLine(")");
            }     
        }
    }
}
