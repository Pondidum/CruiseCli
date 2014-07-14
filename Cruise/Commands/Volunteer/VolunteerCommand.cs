using System;
using Cruise.Transport;
using FubuCore.CommandLine;

namespace Cruise.Commands.Volunteer
{
	public class VolunteerCommand : FubuCommand<VolunteerInputModel>
	{
		private readonly SingleProjectAction _singleAction;

		public VolunteerCommand(ITransportModel transport, SingleProjectAction singleAction)
		{
			_singleAction = singleAction;
			singleAction.Action = (s, p) => transport.VolunteerToFixProject(s, p, Environment.UserName);

			Usage("Volunteers to fix a server/project")
				.Arguments(a => a.Project);
		}

		public override bool Execute(VolunteerInputModel input)
		{
			var spec = new ProjectNameParser().Parse(input.Project);

			return _singleAction.Execute(spec);
		}
	}
}
