using Cruise.Infrastructure;
using Cruise.Transport;
using FubuCore;
using FubuCore.CommandLine;

namespace Cruise.Commands.Run
{
	public class RunCommand : FubuCommand<RunInputModel>
	{
		private readonly IResponse _writer;
		private readonly ITransportModel _transport;

		public RunCommand(IResponse writer, ITransportModel transport)
		{
			_writer = writer;
			_transport = transport;

			Usage("Forces a build for the given server/project")
				.Arguments(a => a.Project);
		}

		public override bool Execute(RunInputModel input)
		{
			if (input.Project.IsEmpty())
			{
				return false;
			}

			return true;
		}
	}
}