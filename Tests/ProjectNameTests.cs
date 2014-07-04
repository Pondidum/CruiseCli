using Cruise;
using Should;
using Xunit;

namespace Tests
{
	public class ProjectNameTests
	{

		[Fact]
		public void When_the_server_and_project_are_both_null()
		{
			var name = new ProjectName(null, null);

			name.IsBlank.ShouldBeTrue();
			name.HasServer.ShouldBeFalse();
			name.HasProject.ShouldBeFalse();
		}

		[Fact]
		public void When_the_server_and_project_are_both_empty()
		{
			var name = new ProjectName(string.Empty, string.Empty);

			name.IsBlank.ShouldBeTrue();
			name.HasServer.ShouldBeFalse();
			name.HasProject.ShouldBeFalse();
		}

		[Fact]
		public void When_the_server_has_a_value_and_project_is_empty()
		{
			var name = new ProjectName("primary", null);

			name.IsBlank.ShouldBeFalse();
			name.HasServer.ShouldBeTrue();
			name.HasProject.ShouldBeFalse();
		}

		[Fact]
		public void When_the_server_is_empty_and_the_project_has_a_value()
		{
			var name = new ProjectName(string.Empty, "test");

			name.IsBlank.ShouldBeFalse();
			name.HasServer.ShouldBeFalse();
			name.HasProject.ShouldBeTrue();
		}

		[Fact]
		public void When_the_server_and_project_have_values()
		{
			var name = new ProjectName("primary", "tetst");

			name.IsBlank.ShouldBeFalse();
			name.HasServer.ShouldBeTrue();
			name.HasProject.ShouldBeTrue();
		}
	}
}
