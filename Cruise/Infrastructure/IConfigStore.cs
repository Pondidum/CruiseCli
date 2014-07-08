using System.IO;

namespace Cruise.Infrastructure
{
	public interface IConfigStore
	{
		void Write(Stream contents);
		Stream Read();
	}
}
