using System.Collections.Generic;
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
			if (input.VerboseFlag)
			{
				_storage.Servers.Each(server => _writer.Write(new GenericModel("    {0,-12}{1}", server.Name, server.Url)));
			}
			else
			{
				_storage.Servers.Each(server => _writer.Write(new GenericModel("    {0}", server.Name)));
			}

			return true;
		}
	}
}