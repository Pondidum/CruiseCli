using System;
using System.Linq;

namespace Tests.Commands
{
	public class ServerCommandUsageTests : CommandUsageTestBase
	{
		public ServerCommandUsageTests()
		{
			Succeeds("server");
			Succeeds("server", "list");
			Succeeds("server", "add", "local", "http://localhost:21234/CruiseManager.rem");
			Succeeds("server", "remove", "local");
		}
	}
}
