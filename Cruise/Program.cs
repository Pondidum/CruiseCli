using System;
using Cruise.Storage;
using FubuCore.CommandLine;
using StructureMap;
using StructureMap.Graph;

namespace Cruise
{
	class Program
	{
		static int Main(string[] args)
		{
			try
			{
				var container = new Container(config =>
				{
					config.Scan(a =>
					{
						a.TheCallingAssembly();
						a.WithDefaultConventions();
					});

					config
						.For<StorageModel>()
						.Use(x => x.GetInstance<StorageController>().Load())
						.Singleton();
				});


				var executor = new CruiseCommandExecutor(container);
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
