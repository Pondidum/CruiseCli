using Cruise.Commands.Volenteer;
using Xunit;

namespace Tests.Commands.VolenteerCommandTests
{
	public class VolenteerCommandUsageTests : CommandUsageTestBase<VolenteerCommand>
	{
		[Fact]
		public void When_calling_with_project_name_succeeds()
		{
			Succeeds("volenteer", "development");
		}

		[Fact]
		public void When_calling_with_a_server_and_project_name_succeeds()
		{
			Succeeds("volenteer", "primary/development");
		}

		[Fact]
		public void Calling_with_no_args_fails()
		{
			Fails("volenteer");
		} 
	}
}
