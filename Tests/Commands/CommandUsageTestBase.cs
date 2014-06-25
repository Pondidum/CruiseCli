using System.Collections.Generic;
using Cruise;
using Should;
using Xunit;

namespace Tests.Commands
{
	public abstract class CommandUsageTestBase
	{
		private readonly List<string[]> _usages;

		protected CommandUsageTestBase()
		{
			_usages = new List<string[]>();
		}

		protected void Add(params string[] input)
		{
			_usages.Add(input);
		}

		[Fact]
		public void All_usages_run_successfully()
		{
			var executor = new CruiseCommandExecutor();

			_usages.ForEach(command => executor.Execute(command).ShouldBeTrue());
		}
	}
}
