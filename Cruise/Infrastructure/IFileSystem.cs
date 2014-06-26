using System.IO;

namespace Cruise.Infrastructure
{
	public interface IFileSystem
	{
		void WriteFile(string path, Stream contents);
		Stream ReadFile(string path);
	}
}
