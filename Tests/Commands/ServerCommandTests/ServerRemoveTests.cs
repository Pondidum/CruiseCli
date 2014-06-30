using Cruise.Commands.Server;
using Cruise.Infrastructure;
using Cruise.Storage;
using NSubstitute;
using Xunit;

namespace Tests.Commands.ServerCommandTests
{
	public class ServerRemoveTests
	{
		private readonly ServerCommand _command;
		private readonly IStorageModel _storage;
		private readonly IResponse _response;
		private readonly ISaveStorageModelCommand _save;

		public ServerRemoveTests()
		{
			_save = Substitute.For<ISaveStorageModelCommand>();
			_storage = Substitute.For<IStorageModel>();
			_response = Substitute.For<IResponse>();

			_command = new ServerCommand(_save, _storage, _response);
		}

		[Fact]
		public void When_removing_a_non_registered_server()
		{
			_storage.IsRegistered("test").Returns(false);

			var input = new ServerInputModel
			{
				RemoveFlag= true,
				Name = "test",
				Url = "http://example.com"
			};

			_command.Execute(input);

			_storage.DidNotReceive().UnRegister("test");
			_save.DidNotReceive().Execute(_storage);
		}

		[Fact]
		public void When_removing_a_registered_server()
		{
			_storage.IsRegistered("test").Returns(true);

			var input = new ServerInputModel
			{
				RemoveFlag = true,
				Name = "test",
				Url = "http://example.com"
			};

			_command.Execute(input);

			_storage.Received().UnRegister("test");
			_save.Received().Execute(_storage);
		}
	}
}
