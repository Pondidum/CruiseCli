using Cruise.Infrastructure;
using Cruise.Transport;
using FubuCore.CommandLine;

namespace Cruise.Commands.Run
{
	public class RunCommand : FubuCommand<RunInputModel>
	{
		public RunCommand(IResponse writer, ITransportModel transport)
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