using Should;
using Xunit;

namespace Tests.ProjectNameParser
{
	public class ParsingABlankName : ProjectNameParserTestBase
	{
		public ParsingABlankName()
		{
			Execute("");
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
	}
}
