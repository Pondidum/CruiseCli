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
		private readonly ListServerCommandAction _command;
		private readonly FakeStorageModel _storage;
		private readonly IResponseWriter _writer;
		private readonly ISaveStorageModelCommand _save;

		public ServerListTests()
		{
			_save = Substitute.For<ISaveStorageModelCommand>();
			_storage = new FakeStorageModel();
			_writer = Substitute.For<IResponseWriter>();

			_command = new ListServerCommandAction(_storage, _writer);
		}

		[Fact]
		public void When_there_are_no_registered_servers()
		{
			_command.Execute(new ServerInputModel());

			_writer.DidNotReceive().Write(Arg.Any<string>(), Arg.Any<object[]>());
		}

		[Fact]
		public void When_there_is_one_registered_server()
		{
			_storage.Insert(new ServerDetails("test", new Uri("http://example.com")));

			_command.Execute(new ServerInputModel());

			_writer.Received().Write(Arg.Any<string>(), "test");
		}

		[Fact]
		public void When_there_are_two_registered_servers()
		{
			_storage.Insert(new ServerDetails("first", new Uri("http://example.com")));
			_storage.Insert(new ServerDetails("second", new Uri("http://example.com")));

			_command.Execute(new ServerInputModel());

			_writer.Received().Write(Arg.Any<string>(), "first");
			_writer.Received().Write(Arg.Any<string>(), "second");
		}

		[Fact]
		public void When_the_verbose_flag_is_set()
		{
			_storage.Insert(new ServerDetails("first", new Uri("http://f.example.com")));
			_storage.Insert(new ServerDetails("second", new Uri("http://s.example.com")));

			_command.Execute(new ServerInputModel {VerboseFlag = true});

			_writer.Received().Write(Arg.Any<string>(), "first", new Uri("http://f.example.com"));
			_writer.Received().Write(Arg.Any<string>(), "second", new Uri("http://s.example.com"));
		}
	}
}
