using System;
using System.Collections.Generic;
using System.Linq;
using Cruise.Infrastructure;
using Cruise.Storage;
using Cruise.Transport;
using FubuCore;
using FubuCore.CommandLine;

namespace Cruise.Commands.Status
{
	public class StatusCommand : FubuCommand<StatusInputModel>
	{
		private readonly IStorageModel _storage;
		private readonly ITransportModel _transport;
		private readonly IResponseWriter _writer;

		public StatusCommand(IResponseWriter writer, IStorageModel storage, ITransportModel transport)
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

			var toDisplay = new Dictionary<string, IEnumerable<IProject>>();

			if (projectSpec.IsBlank)
			{
				toDisplay =_storage.Servers.ToDictionary(s => s.Name, s => _transport.GetProjects(s));
			}
			else if (projectSpec.HasServer && projectSpec.HasProject)
			{
				var projects = _transport.GetProjects(_storage.GetServerByName(projectSpec.Server));
				var project = projects.FirstOrDefault(p => p.Name.EqualsIgnoreCase(projectSpec.Project));

				if (project != null)
				{
					toDisplay.Add(projectSpec.Server, new[] { project });
				}
			}
			else if (projectSpec.HasServer)
			{
				toDisplay.Add(projectSpec.Server, _transport.GetProjects(_storage.GetServerByName(projectSpec.Server)));
			}
			else if (projectSpec.HasProject)
			{
				var projects = _storage
					.Servers
					.ToDictionary(
						s => s.Name,
						s => _transport.GetProjects(s).Where(p => p.Name.EqualsIgnoreCase(projectSpec.Project))
					)
					.Where(pair => pair.Value.Any());

				projects.Each(set => toDisplay.Add(set.Key, set.Value));
			}


			toDisplay.OrderBy(p => p.Key).Each(detail =>
			{
				_writer.Write("{0}:", detail.Key);

				detail.Value.Each(project => _writer.Write("    {0,-12}{1}", project.Status, project.Name));

				_writer.Write("");
			});

			return true;
		}
	}
}