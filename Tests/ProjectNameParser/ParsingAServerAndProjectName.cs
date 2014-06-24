using Should;
using Xunit;

namespace Tests
{
	public class ParsingAServerAndProjectName : ProjectNameParserTestBase
	{
		public ParsingAServerAndProjectName()
		{
			Execute("primary/development");
		}

		[Fact]
		public void Server_is_primary()
		{
			Result.Server.ShouldEqual("primary");
		}

		[Fact]
		public void Project_is_development()
		{
			Result.Project.ShouldEqual("development");
		}
	}
}