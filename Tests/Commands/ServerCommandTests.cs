using System.Linq;
using Cruise;
using Should;
using Xunit;

namespace Tests.Commands
{
	public class ServerCommandTests
	{
		private bool _result;

		private void Execute(params string[] args)
		{
			var executor = new CruiseCommandExecutor();
			_result = executor.Execute(new []{ "server" }.Union(args).ToArray());
		}

		[Fact]
		public void No_arguments()
		{
			Execute();
			_result.ShouldBeTrue();
		}

		[Fact]
		public void List()
		{
			Execute("list");
			_result.ShouldBeTrue();
		}

		[Fact]
		public void Add_with_arguments()
		{
			Execute("add", "local", "http://localhost:21234/CruiseManager.rem");
			_result.ShouldBeTrue();
		}

		[Fact]
		public void Remove_with_arguments()
		{
			Execute("remove", "local");
			_result.ShouldBeTrue();
		}
	}
}
