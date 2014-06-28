using System.IO;

namespace Cruise.Infrastructure
{
	public interface IFileSystem
	{
		string HomePath { get; }

		void WriteFile(string path, Stream contents);
		Stream ReadFile(string path);
		bool FileExists(string path);
	}
}
