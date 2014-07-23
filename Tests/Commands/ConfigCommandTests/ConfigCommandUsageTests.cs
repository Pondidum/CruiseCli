using Cruise.Commands.Config;
using Xunit;

namespace Tests.Commands.ConfigCommandTests
{
	public class ConfigCommandUsageTests : CommandUsageTestBase<ConfigCommand>
	{
		[Fact]
		public void When_called_with_the_color_flag_and_no_args()
		{
			Succeeds("config", "--color");
		}

		[Fact]
		public void When_called_with_the_color_flag_and_a_category()
		{
			Succeeds("config", "--color", "warning");
		}

		[Fact]
		public void When_called_with_the_color_flag_a_cateogry_and_value()
		{
			Succeeds("config", "--color", "warning", "pink");
		}
	}
}
