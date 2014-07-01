using System;

namespace Cruise.Storage
{
	public class ServerDetails : IServerDetails
	{
		public string Name { get; private set; }
		public Uri Url { get; private set; }

		public ServerDetails(string name, Uri url)
		{
			Name = name;
			Url = url;
		}
	}
}
