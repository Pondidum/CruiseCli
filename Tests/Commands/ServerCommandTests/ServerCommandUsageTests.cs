using Cruise.Commands.Server;

namespace Tests.Commands.ServerCommandTests
{
	public class ServerCommandUsageTests : CommandUsageTestBase<ServerCommand>
	{
		public ServerCommandUsageTests()
		{
			Succeeds("server");
			Succeeds("server", "list");
			Succeeds("server", "--add", "local", "http://localhost:21234/CruiseManager.rem");
			Succeeds("server", "--remove", "local");

			Fails("server", "--add");
			Fails("server", "--add", "local");
			Fails("server", "--add", "http://localhost:21234/CruiseManager.rem");

			Fails("server", "--remove");
			Fails("server", "--remove", "local", "http://localhost:21234/CruiseManager.rem");
		}
	}
}
