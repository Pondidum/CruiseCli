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
		private readonly CommandFactory _factory;

		protected CommandUsageTestBase()
		{
			_succeedingInput = new List<string[]>();
			_failingInput = new List<string[]>();

			_factory = new CommandFactoryBuilder(new Container(new CruiseRegistry())).Build();
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
			_succeedingInput.ForEach(command => _factory
				.BuildRun(command)
				.Command
				.ShouldBeType<TCommand>());
		}

		[Fact]
		public void All_failing_usages_fail()
		{
			_failingInput.ForEach(command => _factory
				.BuildRun(command)
				.Command
				.ShouldBeType<HelpCommand>());

		}
	}
}
