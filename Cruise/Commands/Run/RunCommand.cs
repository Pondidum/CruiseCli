using FubuCore.CommandLine;

namespace Cruise.Commands.Run
{
	public class RunCommand : FubuCommand<RunInputModel>
	{
		public RunCommand()
		{
			Usage("Forces a build for the given server/project")
				.Arguments(a => a.Project);
		}

		public override bool Execute(RunInputModel input)
		{
			return true;
		}
	}
}