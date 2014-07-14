using System;
using Cruise.Commands.Server;
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
		private readonly IResponse _response;
		private readonly ISaveStorageModelCommand _save;

		public ServerAddTests()
		{
			_save = Substitute.For<ISaveStorageModelCommand>();
			_storage = Substitute.For<IStorageModel>();
			_response = Substitute.For<IResponse>();

			_command = new AddServerCommandAction(_save, _storage, _response);
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