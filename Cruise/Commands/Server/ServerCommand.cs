using FubuCore.CommandLine;

namespace Cruise.Commands.Server
{
	public class ServerCommand : FubuCommand<ServerInputModel>
	{
		public ServerCommand()
		{
			Usage("Lists all registered servers")
				.Arguments()
				.ValidFlags();

			Usage("Adds a server.")
				.Arguments(a => a.Name, a => a.Url)
				.ValidFlags(a => a.AddFlag);

			Usage("Removes a server.")
				.Arguments(a => a.Name)
				.ValidFlags(a => a.RemoveFlag);
		}

		public override bool Execute(ServerInputModel input)
		{
			return true;
		}
	}
}
