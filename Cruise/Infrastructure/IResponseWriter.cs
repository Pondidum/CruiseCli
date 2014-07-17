namespace Cruise.Infrastructure
{
	public interface IResponseWriter
	{
		void Write(string format, params object[] args);
	}
}
