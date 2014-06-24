using Should;
using Xunit;

namespace Tests.Commands
{
	public class StatusWithNoArguments: CommandTestBase
	{
		public StatusWithNoArguments()
		{
			Execute("status");
		}
	}

	public class StatusWithProjectSpecified : CommandTestBase
	{
		public StatusWithProjectSpecified()
		{
			Execute("status", "development");
		}
	}

	public class StatusWithServerAndProjectSpecified : CommandTestBase
	{
		public StatusWithServerAndProjectSpecified()
		{
			Execute("status", "primary/development");
		}
	}
}
