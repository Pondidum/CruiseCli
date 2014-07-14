using System.Collections.Generic;
using Cruise.Infrastructure;
using Cruise.Storage;

namespace Cruise.Commands.Server
{
	public class ListServerCommandAction : IServerCommandAction
	{
		private readonly IStorageModel _storage;
		private readonly IResponse _response;

		public ListServerCommandAction(IStorageModel storage, IResponse response)
		{
			_storage = storage;
			_response = response;
		}

	
		public bool CanHandle(ServerInputModel input)
		{
			return input.AddFlag == false && input.RemoveFlag == false;
		}

		public bool Execute(ServerInputModel input)
		{
			if (input.VerboseFlag)
			{
				_storage.Servers.Each(server => _response.Write("    {0,-12}{1}", server.Name, server.Url));
			}
			else
			{
				_storage.Servers.Each(server => _response.Write("    {0}", server.Name));
			}

			return true;
		}
	}
}