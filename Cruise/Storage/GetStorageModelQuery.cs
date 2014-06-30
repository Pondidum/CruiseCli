using System.IO;
using Cruise.Infrastructure;
using Newtonsoft.Json;

namespace Cruise.Storage
{
	public class GetStorageModelQuery
	{
		private readonly IFileSystem _fileSystem;
		private readonly string _path;

		public GetStorageModelQuery(IFileSystem fileSystem)
		{
			_fileSystem = fileSystem;
			_path = Path.Combine(_fileSystem.HomePath, ApplicationSettings.Filename);
		}

		public IStorageModel Execute()
		{
			if (_fileSystem.FileExists(_path) == false)
			{
				return new StorageModel(new StorageModelMemento());
			}

			using (var stream = _fileSystem.ReadFile(_path))
			{
				using (var sr = new StreamReader(stream))
				{
					var memento = JsonConvert.DeserializeObject<StorageModelMemento>(sr.ReadToEnd());

					return new StorageModel(memento);
				}
			}
		}
	}
}
