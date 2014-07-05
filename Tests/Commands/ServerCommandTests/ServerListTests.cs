using System;
using System.Collections.Generic;
using System.Linq;
using Cruise.Commands.Server;
using Cruise.Infrastructure;
using Cruise.Storage;
using NSubstitute;
using Xunit;

namespace Tests.Commands.ServerCommandTests
{
	public class ServerListTests
	{
		private readonly ServerCommand _command;
		private readonly FakeStorageModel _storage;
		private readonly IResponse _response;
		private readonly ISaveStorageModelCommand _save;

		public ServerListTests()
		{
			_save = Substitute.For<ISaveStorageModelCommand>();
			_storage = new FakeStorageModel();
			_response = Substitute.For<IResponse>();

			_command = new ServerCommand(_save, _storage, _response);
		}

		[Fact]
		public void When_there_are_no_registered_servers()
		{
			_command.Execute(new ServerInputModel());

			_response.DidNotReceive().Write(Arg.Any<string>(), Arg.Any<object[]>());
		}

		[Fact]
		public void When_there_is_one_registered_server()
		{
			_storage.Insert(new ServerDetails("test", new Uri("http://example.com")));

			_command.Execute(new ServerInputModel());

			_response.Received().Write(Arg.Any<string>(), "test");
		}

		[Fact]
		public void When_there_are_two_registered_servers()
		{
			_storage.Insert(new ServerDetails("first", new Uri("http://example.com")));
			_storage.Insert(new ServerDetails("second", new Uri("http://example.com")));

			_command.Execute(new ServerInputModel());

			_response.Received().Write(Arg.Any<string>(), "first");
			_response.Received().Write(Arg.Any<string>(), "second");
		}
	}
}
