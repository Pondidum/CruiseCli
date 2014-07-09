using Cruise.Commands.Status;
using Xunit;

namespace Tests.Commands.StatusCommandTests
{
	public class StatusCommandUsageTests : CommandUsageTestBase<StatusCommand>
	{
		[Fact]
		public void Calling_with_no_args_succeeds()
		{
			Succeeds("status");
		}

		[Fact]
		public void Calling_with_project_name_succeeds()
		{
			Succeeds("status", "development");
		}

		[Fact]
		public void Calling_with_a_server_and_project_name_succeeds()
		{
			Succeeds("status", "primary/development");
		}

	}
}
