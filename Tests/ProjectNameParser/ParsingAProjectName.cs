using Should;
using Xunit;

namespace Tests.ProjectNameParser
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

		[Fact]
		public void IsBlank()
		{
			Result.IsBlank.ShouldBeFalse();
		}

		[Fact]
		public void HasServer()
		{
			Result.HasServer.ShouldBeFalse();
		}

		[Fact]
		public void HasProject()
		{
			Result.HasProject.ShouldBeTrue();
		}
	}
}