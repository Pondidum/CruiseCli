namespace Tests.Commands
{
	public class RunCommandTests : CommandUsageTestBase
	{
		public RunCommandTests()
		{
			Succeeds("run", "development");
			Succeeds("run", "primary/development");

			Fails("run");
		}
	}
}
