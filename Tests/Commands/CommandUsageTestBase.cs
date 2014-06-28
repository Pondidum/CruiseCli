using System.Collections.Generic;
using Cruise;
using FubuCore.CommandLine;
using Should;
using StructureMap;
using Xunit;

namespace Tests.Commands
{
	public abstract class CommandUsageTestBase<TCommand> where TCommand : IFubuCommand
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
			var factory = new CommandFactoryBuilder(new Container()).Build();

			_succeedingInput.ForEach(command => factory
				.BuildRun(command)
				.Command
				.ShouldBeType<TCommand>());
		}

		[Fact]
		public void All_failing_usages_fail()
		{
			var factory = new CommandFactoryBuilder(new Container()).Build();

			_failingInput.ForEach(command => factory
				.BuildRun(command)
				.Command
				.ShouldBeType<HelpCommand>());

		}
	}
}
