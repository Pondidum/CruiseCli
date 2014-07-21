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
		private readonly IConfigurationModel _configuration;
		private readonly ITransportModel _transport;
		private readonly IResponseWriter _writer;

		public StatusCommand(IResponseWriter writer, IConfigurationModel configuration, ITransportModel transport)
		{
			_configuration = configuration;
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

			var toDisplay = new Dictionary<IServerDetails, IEnumerable<IProject>>();

			if (projectSpec.IsBlank)
			{
				toDisplay = _configuration.Servers.ToDictionary(s => s, s => _transport.GetProjects(s));
			}
			else if (projectSpec.HasServer && projectSpec.HasProject)
			{
				var projects = _transport.GetProjects(_configuration.GetServerByName(projectSpec.Server));
				var project = projects.FirstOrDefault(p => p.Name.EqualsIgnoreCase(projectSpec.Project));

				if (project != null)
				{
					toDisplay.Add(_configuration.GetServerByName(projectSpec.Server), new[] { project });
				}
			}
			else if (projectSpec.HasServer)
			{
				toDisplay.Add(_configuration.GetServerByName(projectSpec.Server), _transport.GetProjects(_configuration.GetServerByName(projectSpec.Server)));
			}
			else if (projectSpec.HasProject)
			{
				var projects = _configuration
					.Servers
					.ToDictionary(
						s => s,
						s => _transport.GetProjects(s).Where(p => p.Name.EqualsIgnoreCase(projectSpec.Project))
					)
					.Where(pair => pair.Value.Any());

				projects.Each(set => toDisplay.Add(set.Key, set.Value));
			}

			var model = new StatusViewModel();

			toDisplay.Each(pair => model.Add(pair.Key, pair.Value));

			_writer.Write(model);

			return true;
		}
	}
}
