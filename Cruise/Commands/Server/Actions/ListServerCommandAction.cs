using Cruise.Infrastructure;
using Cruise.Models;
using Cruise.Storage;

namespace Cruise.Commands.Server
{
	public class ListServerCommandAction : IServerCommandAction
	{
		private readonly IStorageModel _storage;
		private readonly IResponseWriter _writer;

		public ListServerCommandAction(IStorageModel storage, IResponseWriter writer)
		{
			_storage = storage;
			_writer = writer;
		}

	
		public bool CanHandle(ServerInputModel input)
		{
			return input.AddFlag == false && input.RemoveFlag == false;
		}

		public bool Execute(ServerInputModel input)
		{
			_writer.Write(new ListServerViewModel(_storage.Servers, input.VerboseFlag));

			return true;
		}
	}
}
