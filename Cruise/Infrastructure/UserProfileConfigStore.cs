using System;
using System.IO;

namespace Cruise.Infrastructure
{
	public class UserProfileConfigStore : IConfigStore
	{
		private readonly string _path;

		public UserProfileConfigStore()
		{
			var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile); 
			_path = Path.Combine(home, ApplicationSettings.Filename);
		}

		public void Write(Stream contents)
		{
			using (var fs = new FileStream(_path, FileMode.Create, FileAccess.Write))
			{
				contents.CopyTo(fs);
			}
		}

		public Stream Read()
		{
			if (File.Exists(_path))
			{
				return new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read);
			}

			return Stream.Null;
		}
	}
}
