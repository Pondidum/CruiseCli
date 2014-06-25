using Cruise;
using Should;
using Xunit;

namespace Tests.Commands
{
	public abstract class CommandTestBase
	{
		private readonly CruiseCommandExecutor _executor;
		public bool Result { get; private set; }

		protected CommandTestBase()
		{
			_executor = new CruiseCommandExecutor();
		}

		protected void Execute(params string[] input)
		{
			Result = _executor.Execute(input);
		}

		[Fact]
		public void The_command_runs_successfully()
		{
			Result.ShouldBeTrue();
		}
	}
}
