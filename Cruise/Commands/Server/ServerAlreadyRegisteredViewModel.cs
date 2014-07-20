namespace Cruise.Commands.Server
{
	public class ServerAlreadyRegisteredViewModel
	{
		public string Name { get; private set; }
		public string Url { get; private set; }

		public ServerAlreadyRegisteredViewModel(string name, string url)
		{
			Name = name;
			Url = url;
		}
	}
}
