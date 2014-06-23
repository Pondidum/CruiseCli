using System;
using FubuCore.CommandLine;

namespace Cruise
{
	class Program
	{
		static int Main(string[] args)
		{
			try
			{
				var factory = new CommandFactory();
				factory.RegisterCommands(typeof(IFubuCommand).Assembly);
				factory.RegisterCommands(typeof(Program).Assembly);

				var executor = new CommandExecutor(factory);
				var success = executor.Execute(args);

				return success ? 0 : 1;
			}
			catch (CommandFailureException e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("ERROR: " + e.Message);
				Console.ResetColor();
				return 1;
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("ERROR: " + ex);
				Console.ResetColor();
				return 1;
			}
		}
	}
}
