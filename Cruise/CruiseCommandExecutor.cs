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
			var container = new Container(c => c.Scan(a =>
			{
				a.TheCallingAssembly();
				a.WithDefaultConventions();
			}));

			var factory = new CommandFactory(new StructureMapAdaptor(container));

			factory.RegisterCommands(typeof(IFubuCommand).Assembly);
			factory.RegisterCommands(typeof(Program).Assembly);

			_executor = new CommandExecutor(factory);
		}

		public bool Execute(string[] args)
		{
			return _executor.Execute(args);
		}
	}
}
