using Cruise;
using FubuCore.CommandLine;
using Xunit;

namespace Tests
{
	public class Scratchpad
	{
		[Fact]
		public void When_parsing_an_input()
		{
			var executor = new CruiseCommandExecutor();

			var result = executor.Execute(new[] { "status", "development" });
		}
	}
}
