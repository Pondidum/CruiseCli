using Cruise.Commands.Run;

namespace Tests.Commands
{
	public class RunCommandTests : CommandUsageTestBase<RunCommand>
	{
		public RunCommandTests()
		{
			Succeeds("run", "development");
			Succeeds("run", "primary/development");

			Fails("run");
		}
	}
}
