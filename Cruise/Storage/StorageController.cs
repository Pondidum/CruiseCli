using Cruise.Infrastructure;

namespace Cruise.Storage
{
	public class StorageController
	{
		private readonly IFileSystem _fileSystem;

		public StorageController(IFileSystem fileSystem)
		{
			_fileSystem = fileSystem;
		}

		public StorageModel Load()
		{
			//using (var stream = _fileSystem.ReadFile(""))
			//{
			//	return JsonConvert.ToObject<StorageModel>(stream);
			//}
			return new StorageModel();
		}

		public void Save(StorageModel model)
		{
			//_fileSystem.WriteFile("", JsonConvert.ToJson(model));
		}
	}
}
