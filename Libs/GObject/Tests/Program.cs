using System;

namespace GObject.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running Tests\n");
            ObjectTests.TestSimple();
            ObjectTests.TestSubclass();
        }
    }
}
