using System;

namespace Cruise.Storage
{
	public class ServerAlreadyRegisteredException : Exception
	{
		public ServerAlreadyRegisteredException(string serverName, Uri serverUrl)
			: base(string.Format("The server {0} ({1}) has already been registered.", serverName, serverUrl))
		{
			
		}
	}
}
