using System;
using System.Collections.Generic;
using System.IO;
using Cruise.Infrastructure;
using Newtonsoft.Json;

namespace Cruise.Storage
{
	public class StorageController
	{
		private const string Filename = "Cruise.config";

		private readonly IFileSystem _fileSystem;
		private readonly string _path;

		public StorageController(IFileSystem fileSystem)
		{
			_fileSystem = fileSystem;
			_path = Path.Combine(_fileSystem.HomePath, Filename);
		}

		public StorageModel Load()
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

		public void Save(StorageModel model)
		{
			var json = JsonConvert.SerializeObject(model);

			using (var ms = new MemoryStream())
			using (var writer = new StreamWriter(ms))
			{
				writer.Write(json);
				ms.Position = 0;

				_fileSystem.WriteFile(_path, ms);
			}
		}

		public class StorageModelMemento
		{
			public Dictionary<string, Uri> Servers { get; set; }

			public StorageModelMemento()
			{
				Servers= new Dictionary<string, Uri>();
			}
		}
	}
}
