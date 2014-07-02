using Should;
using Xunit;

namespace Tests.ProjectNameParser
{
	public class ParsingANullName : ProjectNameParserTestBase
	{
		public ParsingANullName()
		{
			Execute(null);
		}

		[Fact]
		public void Server_is_blank()
		{
			Result.Server.ShouldBeEmpty();
		}

		[Fact]
		public void Project_is_blank()
		{
			Result.Project.ShouldBeEmpty();
		}

		[Fact]
		public void IsBlank()
		{
			Result.IsBlank.ShouldBeTrue();
		}

		[Fact]
		public void HasServer()
		{
			Result.HasServer.ShouldBeFalse();
		}

		[Fact]
		public void HasProject()
		{
			Result.HasProject.ShouldBeFalse();
		}

	}
}
