using Exercice01.Core;
using System;

namespace Exercise01.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Write("Enter Number: ");

			var input = Console.ReadLine();

			Console.WriteLine(input.ToWords());

			
			Console.ReadKey();
		}

	}
}
