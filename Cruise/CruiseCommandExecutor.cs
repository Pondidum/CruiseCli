using FubuCore.CommandLine;
using StructureMap;

namespace Cruise
{
	public class CruiseCommandExecutor
	{
		private readonly CommandExecutor _executor;

		public CruiseCommandExecutor(IContainer container)
		{
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
