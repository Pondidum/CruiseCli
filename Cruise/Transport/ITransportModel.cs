namespace Cruise.Transport
{
	public interface ITransportModel
	{
		IServer GetServer(string serverName);
	}
}
