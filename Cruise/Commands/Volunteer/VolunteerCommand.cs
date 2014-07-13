using System;
using System.Collections.Generic;
using System.Linq;
using Cruise.Infrastructure;
using Cruise.Storage;
using Cruise.Transport;
using FubuCore;
using FubuCore.CommandLine;

namespace Cruise.Commands.Volunteer
{
	public class VolunteerCommand : FubuCommand<VolunteerInputModel>
	{
		private readonly IResponse _response;
		private readonly IStorageModel _storage;
		private readonly ITransportModel _transport;

		public VolunteerCommand(IResponse response, IStorageModel storage, ITransportModel transport)
		{
			_response = response;
			_storage = storage;
			_transport = transport;

			Usage("Volunteers to fix a server/project")
				.Arguments(a => a.Project);
		}

		public override bool Execute(VolunteerInputModel input)
		{
			var spec = new ProjectNameParser().Parse(input.Project);

			if (spec.IsBlank || spec.HasProject == false)
			{
				_response.Write("Error, you must specify a Project name.");
				_response.Write("");

				return false;
			}

			var serverDetails = _storage
				.Servers
				.Where(server => _transport
					.GetProjects(server)
					.Any(p => p.Name.EqualsIgnoreCase(spec.Project)))
				.ToList();

			if (serverDetails.Count > 1)
			{
				_response.Write("Error, ambiguous Project name.");
				_response.Write("Did you mean:");

				serverDetails
					.Select(detail => new ProjectName(detail.Name, spec.Project))
					.Each(detail => _response.Write("    {0}", detail));
				_response.Write("");

				return false;
			}

			_transport.VolunteerToFixProject(serverDetails.First(), spec.Project, Environment.UserName);

			return false;
		}
	}
}
