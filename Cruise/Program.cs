using System;
using FubuCore.CommandLine;
using StructureMap;

namespace Cruise
{
	class Program
	{
		static int Main(string[] args)
		{
			try
			{
				var container = new Container(new CruiseRegistry());
				var factoryBuilder = new CommandFactoryBuilder(container);

				var executor = new CommandExecutor(factoryBuilder.Build());
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
