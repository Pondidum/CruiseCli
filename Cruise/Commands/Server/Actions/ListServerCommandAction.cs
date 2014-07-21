using Cruise.Infrastructure;
using Cruise.Storage;

namespace Cruise.Commands.Server.Actions
{
	public class ListServerCommandAction : IServerCommandAction
	{
		private readonly IConfigurationModel _configuration;
		private readonly IResponseWriter _writer;

		public ListServerCommandAction(IConfigurationModel configuration, IResponseWriter writer)
		{
			_configuration = configuration;
			_writer = writer;
		}

	
		public bool CanHandle(ServerInputModel input)
		{
			return input.AddFlag == false && input.RemoveFlag == false;
		}

		public bool Execute(ServerInputModel input)
		{
			_writer.Write(new ListServerViewModel(_configuration.Servers, input.VerboseFlag));

			return true;
		}
	}
}
