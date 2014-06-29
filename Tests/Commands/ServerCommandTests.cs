using System;
using System.Collections.Generic;
using System.Linq;
using Cruise.Commands.Server;
using Cruise.Infrastructure;
using Cruise.Storage;
using NSubstitute;
using Xunit;

namespace Tests.Commands
{
	public class ServerCommandUsageTests : CommandUsageTestBase<ServerCommand>
	{
		public ServerCommandUsageTests()
		{
			Succeeds("server");
			Succeeds("server", "list");
			Succeeds("server", "--add", "local", "http://localhost:21234/CruiseManager.rem");
			Succeeds("server", "--remove", "local");

			Fails("server", "--add");
			Fails("server", "--add", "local");
			Fails("server", "--add", "http://localhost:21234/CruiseManager.rem");

			Fails("server", "--remove");
			Fails("server", "--remove", "local", "http://localhost:21234/CruiseManager.rem");
		}
	}

	public class ServerListTests
	{
		private readonly ServerCommand _command;
		private readonly IStorageModel _storage;
		private readonly IResponse _response;

		public ServerListTests()
		{
			_storage = Substitute.For<IStorageModel>();
			_response = Substitute.For<IResponse>();

			_command = new ServerCommand(_storage, _response);
		}

		[Fact]
		public void When_there_are_no_registered_servers()
		{
			_storage.Servers.Returns(Enumerable.Empty<KeyValuePair<string, Uri>>());

			_command.Execute(new ServerInputModel());

			_response.DidNotReceive().Write(Arg.Any<string>(), Arg.Any<object[]>());
		}

		[Fact]
		public void When_there_is_one_registered_server()
		{
			_storage.Servers.Returns(new[] { new KeyValuePair<string, Uri>("test", new Uri("http://example.com")) });

			_command.Execute(new ServerInputModel());

			_response.Received().Write(Arg.Any<string>(), "test");
		}

		[Fact]
		public void When_there_are_two_registered_servers()
		{
			_storage.Servers.Returns(new[]
			{
				new KeyValuePair<string, Uri>("first", new Uri("http://example.com")),
				new KeyValuePair<string, Uri>("second", new Uri("http://example.com"))
			});

			_command.Execute(new ServerInputModel());

			_response.Received().Write(Arg.Any<string>(), "first");
			_response.Received().Write(Arg.Any<string>(), "second");
		}
	}
}
