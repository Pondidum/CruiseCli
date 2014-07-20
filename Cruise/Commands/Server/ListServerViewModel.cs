using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruise.Storage;

namespace Cruise.Commands.Server
{
	public class ListServerViewModel
	{
		public List<IServerDetails> Servers { get; private set; }
		public bool Verbose { get; private set; }

		public ListServerViewModel(IEnumerable<IServerDetails> servers, bool verbose)
		{
			Servers = servers.ToList();
			Verbose = verbose;
		}
	}
}
