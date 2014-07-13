using Cruise.Commands.Volunteer;
using Xunit;

namespace Tests.Commands.VolunteerCommandTests
{
	public class VolunteerCommandUsageTests : CommandUsageTestBase<VolunteerCommand>
	{
		[Fact]
		public void When_calling_with_project_name_succeeds()
		{
			Succeeds("volunteer", "development");
		}

		[Fact]
		public void When_calling_with_a_server_and_project_name_succeeds()
		{
			Succeeds("volunteer", "primary/development");
		}

		[Fact]
		public void Calling_with_no_args_fails()
		{
			Fails("volunteer");
		} 
	}
}
