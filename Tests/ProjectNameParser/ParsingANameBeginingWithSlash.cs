using Should;
using Xunit;

namespace Tests.ProjectNameParser
{
	public class ParsingANameBeginingWithSlash : ProjectNameParserTestBase
	{
		public ParsingANameBeginingWithSlash()
		{
			Execute("/development");
		}

		[Fact]
		public void Server_is_empty()
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
