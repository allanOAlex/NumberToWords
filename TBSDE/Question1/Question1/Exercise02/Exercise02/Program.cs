using Exercise01;
using System;
using System.Numerics;

namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Number: ");

            var input = Console.ReadLine();
            input = input.Replace(",", "");
            var param = BigInteger.Parse(input);

            Console.WriteLine(param.Towards());

            Console.ReadKey();
        }
    }
}
