using System;
using System.Linq;

namespace Tests.Commands
{
	public class ServerCommandUsageTests : CommandUsageTestBase
	{
		public ServerCommandUsageTests()
		{
			Add("server");
			Add("server", "list");
			Add("server", "add", "local", "http://localhost:21234/CruiseManager.rem");
			Add("server", "remove", "local");
		}
	}
}
