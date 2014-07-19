using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruise.Storage;
using Cruise.Transport;

namespace Cruise.Commands.Status
{
	public class StatusViewModel
	{
		private readonly Dictionary<IServerDetails, List<IProject>> _servers;

		public StatusViewModel()
		{
			_servers = new Dictionary<IServerDetails, List<IProject>>();
		}

		public void Add(IServerDetails server, IEnumerable<IProject> projects)
		{
			if (_servers.ContainsKey(server) == false)
			{
				_servers[server] = new List<IProject>();
			}

			var storedProjects = _servers[server];
			var toAdd = projects.Except(storedProjects).ToList();

			storedProjects.AddRange(toAdd);
		}

		public override string ToString()
		{
			var sb = new StringBuilder();

			var ordered = _servers
				.OrderBy(p => p.Key.Name)
				.ToList();

			ordered.ForEach(detail =>
			{
				sb.AppendFormat("{0}:", detail.Key).AppendLine();

				var projects = detail
					.Value
					.OrderBy(p => p.Name)
					.ToList();

				projects.ForEach(project => sb.AppendFormat("    {0,-12}{1}", project.Status, project.Name).AppendLine());

				sb.AppendLine();
			});

			return sb.ToString();
		}
	}
}
