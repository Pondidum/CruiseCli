using Cruise.Infrastructure;
using Cruise.Storage;
using Cruise.Transport;
using FubuCore.CommandLine;

namespace Cruise.Commands.Volenteer
{
	public class VolenteerCommand : FubuCommand<VolenteerInputModel>
	{
		private readonly IResponse _response;
		private readonly IStorageModel _storage;
		private readonly ITransportModel _transport;

		public VolenteerCommand(IResponse response, IStorageModel storage, ITransportModel transport)
		{
			_response = response;
			_storage = storage;
			_transport = transport;

			Usage("Volenteers to fix a server/project")
				.Arguments(a => a.Project);
		}

		public override bool Execute(VolenteerInputModel input)
		{
			throw new System.NotImplementedException();
		}
	}
}
