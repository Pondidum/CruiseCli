using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruise.Storage;

namespace Cruise.Commands.Server
{
	public class ListServerViewModel
	{
		public List<IServerDetails> Servers { get; private set; }
		private readonly bool _verbose;

		public ListServerViewModel(IEnumerable<IServerDetails> servers, bool verbose)
		{
			Servers = servers.ToList();
			_verbose = verbose;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			
			if (_verbose)
			{
				Servers.ForEach(server => sb.AppendFormat("    {0,-12}{1}", server.Name, server.Url).AppendLine());
			}
			else
			{
				Servers.ForEach(server => sb.AppendFormat("    {0}", server.Name).AppendLine());
			}

			return sb.ToString();
		}
	}
}
