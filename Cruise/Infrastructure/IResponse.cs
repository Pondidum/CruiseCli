namespace Cruise.Infrastructure
{
	public interface IResponse
	{
		void Write(string format, params object[] args);
	}
}
