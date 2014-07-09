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
		private readonly CommandFactory _factory;

		protected CommandUsageTestBase()
		{
			_factory = new CommandFactoryBuilder(new Container(new CruiseRegistry())).Build();
		}

		protected void Succeeds(params string[] input)
		{
			_factory
				.BuildRun(input)
				.Command
				.ShouldBeType<TCommand>();
		}

		protected void Fails(params string[] input)
		{
			_factory
				.BuildRun(input)
				.Command
				.ShouldBeType<HelpCommand>();
		}
	}
}
