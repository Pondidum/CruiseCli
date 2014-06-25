using Should;
using Xunit;

namespace Tests.Commands
{
	public class StatusCommandUsageTests : CommandUsageTestBase
	{
		public StatusCommandUsageTests()
		{
			Add("status");
			Add("status", "development");
			Add("status", "primary/development");
		}
	}
}
