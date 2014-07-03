using Cruise.Commands.Run;

namespace Tests.Commands.RunCommandTests
{
	public class RunCommandUsageTests : CommandUsageTestBase<RunCommand>
	{
		public RunCommandUsageTests()
		{
			Succeeds("run", "development");
			Succeeds("run", "primary/development");

			Fails("run");
		}
	}
}
