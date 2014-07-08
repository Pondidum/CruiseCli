using System.IO;
using Cruise.Infrastructure;
using Newtonsoft.Json;

namespace Cruise.Storage
{
	public class SaveStorageModelCommand : ISaveStorageModelCommand
	{
		private readonly IFileSystem _fileSystem;

		public SaveStorageModelCommand(IFileSystem fileSystem)
		{
			_fileSystem = fileSystem;
		}

		public void Execute(IStorageModel model)
		{
			var memento = model.ToMemento();

			var json = JsonConvert.SerializeObject(memento);

			using (var stream = new MemoryStream())
			using (var writer = new StreamWriter(stream))
			{
				writer.Write(json);
				writer.Flush();

				stream.Position = 0;

				_fileSystem.WriteFile(
					Path.Combine(_fileSystem.HomePath, ApplicationSettings.Filename), 
					stream);
			}
		}
	}
}
