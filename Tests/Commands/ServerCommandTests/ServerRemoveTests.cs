using Cruise.Commands.Server;
using Cruise.Infrastructure;
using Cruise.Storage;
using NSubstitute;
using Xunit;

namespace Tests.Commands.ServerCommandTests
{
	public class ServerRemoveTests
	{
		private readonly RemoveServerCommandAction _command;
		private readonly IStorageModel _storage;
		private readonly ISaveStorageModelCommand _save;

		public ServerRemoveTests()
		{
			_save = Substitute.For<ISaveStorageModelCommand>();
			_storage = Substitute.For<IStorageModel>();

			_command = new RemoveServerCommandAction(_save, _storage);
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
