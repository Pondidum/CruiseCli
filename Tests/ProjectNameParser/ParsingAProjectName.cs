using Should;
using Xunit;

namespace Tests
{
	public class ParsingAProjectName : ProjectNameParserTestBase
	{
		public ParsingAProjectName()
		{
			Execute("development");
		}

		[Fact]
		public void Server_is_blank()
		{
			Result.Server.ShouldBeEmpty();
		}

		[Fact]
		public void Project_is_development()
		{
			Result.Project.ShouldEqual("development");
		}
	}
}