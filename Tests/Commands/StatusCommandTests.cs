using Should;
using Xunit;

namespace Tests.Commands
{
	public class StatusCommandUsageTests : CommandUsageTestBase
	{
		public StatusCommandUsageTests()
		{
			Succeeds("status");
			Succeeds("status", "development");
			Succeeds("status", "primary/development");
		}
	}
}
