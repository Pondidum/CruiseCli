using System;

namespace Cruise.Storage
{
	public class DefaultColours : Colours
	{
		public DefaultColours()
		{
			Default = Console.ForegroundColor;
			Error = ConsoleColor.Red;
			Warning = ConsoleColor.Yellow;
			Success = ConsoleColor.Green;
		}
	}
}
