using FubuCore.CommandLine;

namespace Cruise
{
	public class CruiseCommandExecutor
	{
		private readonly CommandExecutor _executor;

		public CruiseCommandExecutor()
		{
			var factory = new CommandFactory();
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
