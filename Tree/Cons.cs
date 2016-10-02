// Cons -- Parse tree node class for representing a Cons node

using System;

namespace Tree
{
    public class Cons : Node
    {
        private Node car;
        private Node cdr;
        private Special form;
    
        public Cons(Node a, Node d)
        {
            car = a;
            cdr = d;
            parseList();
        }
    
        // parseList() `parses' special forms, constructs an appropriate
        // object of a subclass of Special, and stores a pointer to that
        // object in variable form.  It would be possible to fully parse
        // special forms at this point.  Since this causes complications
        // when using (incorrect) programs as data, it is easiest to let
        // parseList only look at the car for selecting the appropriate
        // object from the Special hierarchy and to leave the rest of
        // parsing up to the interpreter.
        void parseList()
        {
            // Not sure how we're intended to "look at the car" for the form, so we'll use this in the meantime
            // The Ident cast may not work, however.
            try
            {
                Ident identCar = (Ident)car;

                //Determine special type
                switch (identCar.getName())
                {
                    case "'":
                        form = new Quote();
                        break;
                    case "LAMDA":
                        form = new Lambda();
                        break;
                    case "BEGIN":
                        form = new Begin();
                        break;
                    case "IF":
                        form = new If();
                        break;
                    case "LET":
                        form = new Let();
                        break;
                    case "COND":
                        form = new Cond();
                        break;
                    case "DEFINE":
                        form = new Define();
                        break;
                    case "SET":
                        form = new Set();
                        break; 
                    default:
                        form = new Regular();
                        break;
                }
            }
            catch
            { form = new Regular(); }
        }
 
        public override void print(int n)
        {
            form.print(this, n, false);
        }

        public override void print(int n, bool p)
        {
            form.print(this, n, p);
        }
    }
}

