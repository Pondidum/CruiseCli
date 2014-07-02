using Should;
using Xunit;

namespace Tests.ProjectNameParser
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

		[Fact]
		public void IsBlank()
		{
			Result.IsBlank.ShouldBeFalse();
		}

		[Fact]
		public void HasServer()
		{
			Result.HasServer.ShouldBeTrue();
		}

		[Fact]
		public void HasProject()
		{
			Result.HasProject.ShouldBeTrue();
		}
	}
}