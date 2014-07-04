using System.Collections.Generic;
using System.Linq;
using Cruise.Infrastructure;
using Cruise.Storage;
using Cruise.Transport;
using FubuCore;
using FubuCore.CommandLine;

namespace Cruise.Commands.Run
{
	public class RunCommand : FubuCommand<RunInputModel>
	{
		private readonly IResponse _writer;
		private readonly IStorageModel _storage;
		private readonly ITransportModel _transport;

		public RunCommand(IResponse writer, IStorageModel storage,  ITransportModel transport)
		{
			_writer = writer;
			_storage = storage;
			_transport = transport;

			Usage("Forces a build for the given server/project")
				.Arguments(a => a.Project);
		}

		public override bool Execute(RunInputModel input)
		{
			var projectSpec = new ProjectNameParser().Parse(input.Project);
			
			if (projectSpec.IsBlank || projectSpec.HasProject == false)
			{
				_writer.Write("Error, you must specify a Project name.");
				_writer.Write("");

				return false;
			}

			if (projectSpec.HasServer && projectSpec.HasProject)
			{
				var projects = _transport.GetProjects(projectSpec.Server);

				if (projects.Any(project => project.Name.EqualsIgnoreCase(projectSpec.Project)))
				{
					_transport.TriggerProject(projectSpec.Server, projectSpec.Project);
				}
				else
				{
					_writer.Write("Error, unable to find project '{0}'.", projectSpec);
					_writer.Write("");
					return false;
				}
			}

			var serverDetails = _storage
				.Servers
				.Where(server => _transport
					.GetProjects(server.Name)
					.Any(p => p.Name.EqualsIgnoreCase(projectSpec.Project)))
				.ToList();

			if (serverDetails.Any() == false)
			{
				_writer.Write("Error, unable to find project '{0}'.", projectSpec.Project);
				_writer.Write("");
				return false;
			}

			if (serverDetails.Count > 1)
			{
				_writer.Write("Error, ambiguous Project name.");
				_writer.Write("Did you mean:");

				serverDetails
					.Select(detail => new ProjectName(detail.Name, projectSpec.Project))
					.Each(detail => _writer.Write("    {0}", detail));
				_writer.Write("");

				return false;
			}

			_transport.TriggerProject(serverDetails.First().Name, projectSpec.Project);

			return true;
		}
	}
}