using System;

namespace Cruise.Storage
{
	public class DefaultColors : Colors
	{
		public DefaultColors()
		{
			Default = Console.ForegroundColor;
			Error = ConsoleColor.Red;
			Warning = ConsoleColor.Yellow;
			Success = ConsoleColor.Green;
		}
	}
}
