using System;
using System.IO;

namespace Cruise.Infrastructure
{
	public class FileSystem : IFileSystem
	{
		public string HomePath { get { return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile); } }

		public void WriteFile(string path, Stream contents)
		{
			using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				contents.CopyTo(fs);
			}
		}

		public Stream ReadFile(string path)
		{
			return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
		}
	}
}
