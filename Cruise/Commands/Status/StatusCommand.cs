using FubuCore.CommandLine;

namespace Cruise.Commands.Status
{
	public class StatusCommand : FubuCommand<StatusInputModel>
	{
		public StatusCommand()
		{
			Usage("Lists the status of all projects on all servers")
				.Arguments()
				.ValidFlags();

			Usage("Lists the status for the given server/project")
				.Arguments(a => a.Project);
		}

		public override bool Execute(StatusInputModel input)
		{
			return true;
		}
	}
}