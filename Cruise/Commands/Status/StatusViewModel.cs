using System.Collections.Generic;
using System.Linq;
using Cruise.Storage;
using Cruise.Transport;

namespace Cruise.Commands.Status
{
	public class StatusViewModel
	{
		public Dictionary<IServerDetails, List<IProject>> Servers { get; private set; }

		public StatusViewModel()
		{
			Servers = new Dictionary<IServerDetails, List<IProject>>();
		}

		public void Add(IServerDetails server, IEnumerable<IProject> projects)
		{
			if (Servers.ContainsKey(server) == false)
			{
				Servers[server] = new List<IProject>();
			}

			var storedProjects = Servers[server];
			var toAdd = projects.Except(storedProjects).ToList();

			storedProjects.AddRange(toAdd);
		}
	}
}
