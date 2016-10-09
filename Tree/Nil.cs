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
                
            if (n > 0) // End of a special type
            {
                for (int k = 0; k < (n - 4) / 4; k++)
                    Console.Write("    ");
                Console.Write(")");
            }
            else 
                Console.Write("\b) ");
        }
    }
}
