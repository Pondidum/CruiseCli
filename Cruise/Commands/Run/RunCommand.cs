using Cruise.Transport;
using FubuCore.CommandLine;

namespace Cruise.Commands.Run
{
	public class RunCommand : FubuCommand<RunInputModel>
	{
		private readonly SingleProjectAction _singleAction;

		public RunCommand(ITransportModel transport, SingleProjectAction singleAction)
		{
			_singleAction = singleAction;
			singleAction.Action = transport.TriggerProject;

			Usage("Forces a build for the given server/project")
				.Arguments(a => a.Project);
		}

		public override bool Execute(RunInputModel input)
		{
			return _singleAction.Execute(new ProjectNameParser().Parse(input.Project));
		}
	}
}
