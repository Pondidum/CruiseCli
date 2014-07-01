using System.Collections.Generic;
using Cruise.Infrastructure;
using Cruise.Storage;
using Cruise.Transport;
using FubuCore.CommandLine;

namespace Cruise.Commands.Status
{
	public class StatusCommand : FubuCommand<StatusInputModel>
	{
		private readonly IStorageModel _storage;
		private readonly ITransportModel _transport;
		private readonly IResponse _writer;

		public StatusCommand(IResponse writer, IStorageModel storage, ITransportModel transport)
		{
			_storage = storage;
			_transport = transport;
			_writer = writer;
	
			Usage("Lists the status of all projects on all servers")
				.Arguments()
				.ValidFlags();

			Usage("Lists the status for the given server/project")
				.Arguments(a => a.Project);
		}

		public override bool Execute(StatusInputModel input)
		{
			var projectSpec = new ProjectNameParser().Parse(input.Project);

			if (projectSpec.IsBlank)
			{
				_storage.Servers.Each(detail =>
				{
					_writer.Write("{0}:", detail.Name);

					_transport
						.GetProjects(detail.Name)
						.Each(project => _writer.Write("    {0,-12}{1}", project.Status, project.Name));

					_writer.Write("");
				});
			}
			else
			{
			}

			return true;
		}
	}
}