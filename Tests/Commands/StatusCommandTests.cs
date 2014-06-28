using Cruise.Commands.Status;
using Should;
using Xunit;

namespace Tests.Commands
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
