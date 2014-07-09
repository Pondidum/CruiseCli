using Cruise.Commands.Run;
using Xunit;

namespace Tests.Commands.RunCommandTests
{
	public class RunCommandUsageTests : CommandUsageTestBase<RunCommand>
	{
		[Fact]
		public void When_calling_with_project_name_succeeds()
		{
			Succeeds("run", "development");
		}

		[Fact]
		public void When_calling_with_a_server_and_project_name_succeeds()
		{
			Succeeds("run", "primary/development");
		}

		[Fact]
		public void Calling_with_no_args_fails()
		{
			Fails("run");
		}
	}
}
