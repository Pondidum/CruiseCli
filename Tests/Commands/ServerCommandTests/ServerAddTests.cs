using System;
using Cruise.Commands.Server;
using Cruise.Commands.Server.Actions;
using Cruise.Infrastructure;
using Cruise.Storage;
using NSubstitute;
using Xunit;

namespace Tests.Commands.ServerCommandTests
{
	public class ServerAddTests
	{
		private readonly AddServerCommandAction _command;
		private readonly IStorageModel _storage;
		private readonly IResponseWriter _writer;
		private readonly ISaveStorageModelCommand _save;

		public ServerAddTests()
		{
			_save = Substitute.For<ISaveStorageModelCommand>();
			_storage = Substitute.For<IStorageModel>();
			_writer = Substitute.For<IResponseWriter>();

			_command = new AddServerCommandAction(_save, _storage, _writer);
		}

		[Fact]
		public void When_adding_a_new_server()
		{
			_storage.IsRegistered("test").Returns(false);

			var input = new ServerInputModel
			{
				AddFlag = true,
				Name = "test",
				Url = "http://example.com"
			};

			_command.Execute(input);

			_storage.Received().Register("test", new Uri("http://example.com"));
			_save.Received().Execute(_storage);
		}

		[Fact]
		public void When_adding_a_server_which_already_exists()
		{
			_storage.IsRegistered("test").Returns(true);

			var input = new ServerInputModel
			{
				AddFlag = true,
				Name = "test",
				Url = "http://example.com"
			};

			_command.Execute(input);

			_storage.DidNotReceive().Register("test", new Uri("http://example.com"));
			_save.DidNotReceive().Execute(_storage);
		}
	}
}