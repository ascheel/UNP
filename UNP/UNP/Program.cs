using System;

namespace UNP
{
	class Program
	{
		static void Main(string[] args)
		{
			var handler = new TokenHandling("Art");
			var token = handler.generate_token();
			Console.WriteLine(token);
		}
	}
}
