using FubuCore.CommandLine;

namespace Cruise.Commands.Server
{
	public class ServerCommand : FubuCommand<ServerInputModel>
	{
		private readonly ServerCommandActionFactory _factory;

		public ServerCommand(ServerCommandActionFactory factory)
		{
			_factory = factory;
			
			Usage("Lists all registered servers")
				.Arguments()
				.ValidFlags(f => f.VerboseFlag);

			Usage("Adds a server.")
				.Arguments(a => a.Name, a => a.Url)
				.ValidFlags(f => f.AddFlag, f => f.VerboseFlag);

			Usage("Removes a server.")
				.Arguments(a => a.Name)
				.ValidFlags(f => f.RemoveFlag, f => f.VerboseFlag);
		}

		public override bool Execute(ServerInputModel input)
		{
			return _factory.Execute(input);
		}
	}
}
