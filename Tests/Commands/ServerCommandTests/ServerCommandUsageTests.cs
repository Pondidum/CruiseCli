using Cruise.Commands.Server;
using Xunit;

namespace Tests.Commands.ServerCommandTests
{
	public class ServerCommandUsageTests : CommandUsageTestBase<ServerCommand>
	{
		[Fact]
		public void Calling_with_no_args_succeeds()
		{
			Succeeds("server");
		}
		
		[Fact]
		public void Calling_with_no_args_and_verbose_flag_succeeds()
		{
			Succeeds("server", "--verbose");
		}

		[Fact]
		public void Calling_with_add_name_and_url_succeeds()
		{
			Succeeds("server", "--add", "local", "http://localhost:21234/CruiseManager.rem");
		}

		[Fact]
		public void Calling_with_add_name_and_url_and_verbose_flag_succeeds()
		{
			Succeeds("server", "--add", "--verbose", "local", "http://localhost:21234/CruiseManager.rem");
		}

		[Fact]
		public void Calling_with_remove_and_name_succeeds()
		{
			Succeeds("server", "--remove", "local");
		}

		[Fact]
		public void Calling_with_remove_and_name_and_verbose_flag_succeeds()
		{
			Succeeds("server", "--remove", "--verbose", "local");
		}


		[Fact]
		public void Calling_with_add_and_no_args_fails()
		{
			Fails("server", "--add");
		}

		[Fact]
		public void Calling_with_add_and_only_name_fails()
		{
			Fails("server", "--add", "local");
		}

		[Fact]
		public void Calling_with_add_and_only_url_fails()
		{
			Fails("server", "--add", "http://localhost:21234/CruiseManager.rem");
		}

		[Fact]
		public void Calling_with_remove_and_no_args_fails()
		{
			Fails("server", "--remove");
		}

		[Fact]
		public void Calling_with_remove_name_and_url_fails()
		{
			Fails("server", "--remove", "local", "http://localhost:21234/CruiseManager.rem");
		}
	}
}
