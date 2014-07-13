using Cruise.Infrastructure;
using Cruise.Storage;
using Cruise.Transport;
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

			return false;
		}
	}
}
