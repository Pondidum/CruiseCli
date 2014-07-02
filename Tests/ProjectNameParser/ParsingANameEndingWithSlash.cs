using Should;
using Xunit;

namespace Tests.ProjectNameParser
{
	public class ParsingANameEndingWithSlash : ProjectNameParserTestBase
	{
		public ParsingANameEndingWithSlash()
		{
			Execute("primary/");
		}

		[Fact]
		public void Server_is_primary()
		{
			Result.Server.ShouldEqual("primary");
		}

		[Fact]
		public void Project_is_blank()
		{
			Result.Project.ShouldBeEmpty();
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
			Result.HasProject.ShouldBeFalse();
		}
	}
}
