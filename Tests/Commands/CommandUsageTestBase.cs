using System.Collections.Generic;
using Cruise;
using Should;
using StructureMap;
using Xunit;

namespace Tests.Commands
{
	public abstract class CommandUsageTestBase
	{
		private readonly List<string[]> _succeedingInput;
		private readonly List<string[]> _failingInput;

		protected CommandUsageTestBase()
		{
			_succeedingInput = new List<string[]>();
			_failingInput = new List<string[]>();
		}

		protected void Succeeds(params string[] input)
		{
			_succeedingInput.Add(input);
		}

		protected void Fails(params string[] input)
		{
			_failingInput.Add(input);
		}

		[Fact]
		public void All_succeeding_usages_run_successfully()
		{
			var executor = new CruiseCommandExecutor(new Container());

			_succeedingInput.ForEach(command => executor.Execute(command).ShouldBeTrue(command.Join(" ")));
		}

		[Fact]
		public void All_failing_usages_fail()
		{
			var executor = new CruiseCommandExecutor(new Container());

			_failingInput.ForEach(command => executor.Execute(command).ShouldBeFalse(command.Join(" ")));
		}
	}
}
