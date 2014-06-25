using FubuCore.CommandLine;

namespace Cruise.Commands.Server
{
	public class ServerCommand : FubuCommand<ServerInputModel>
	{
		public ServerCommand()
		{
			Usage("Lists all registered servers")
				.Arguments();

			Usage("Lists all registered servers")
				.Arguments(a => a.Command);

			Usage("Adds a server.")
				.Arguments(a => a.Command, a => a.Name, a => a.Url);

			Usage("Removes a server.")
				.Arguments(a => a.Command, a => a.Name);
		}

		public override bool Execute(ServerInputModel input)
		{
			return true;
		}
	}
}
