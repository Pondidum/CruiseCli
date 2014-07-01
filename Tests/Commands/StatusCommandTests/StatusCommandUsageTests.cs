using Cruise.Commands.Status;

namespace Tests.Commands.StatusCommandTests
{
	public class StatusCommandUsageTests : CommandUsageTestBase<StatusCommand>
	{
		public StatusCommandUsageTests()
		{
			Succeeds("status");
			Succeeds("status", "development");
			Succeeds("status", "primary/development");
		}
	}
}
