using Cruise.Storage;
using FubuCore.CommandLine;
using StructureMap;
using StructureMap.Graph;

namespace Cruise
{
	public class CruiseCommandExecutor
	{
		private readonly CommandExecutor _executor;

		public CruiseCommandExecutor()
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

			var factory = new CommandFactory(new StructureMapAdaptor(container));

			factory.RegisterCommands(typeof(IFubuCommand).Assembly);
			factory.RegisterCommands(typeof(Program).Assembly);

			_executor = new CommandExecutor(factory);

			container.GetInstance<StorageController>().Save(container.GetInstance<StorageModel>());
		}

		public bool Execute(string[] args)
		{
			return _executor.Execute(args);
		}
	}
}
