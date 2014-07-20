namespace Cruise.Commands.Server
{
	public class ServerAlreadyRegisteredViewModel
	{
		private readonly string _name;
		private readonly string _url;

		public ServerAlreadyRegisteredViewModel(string name, string url)
		{
			_name = name;
			_url = url;
		}

		public override string ToString()
		{
			return string.Format("Server {0} ({1}) is already registered.", _name, _url);
		}
	}
}
