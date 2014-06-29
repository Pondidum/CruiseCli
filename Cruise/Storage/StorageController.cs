using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

		public IStorageModel Load()
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

		public void Save(IStorageModel model)
		{
			var memento = StorageModelMemento.FromModel(model);

			var json = JsonConvert.SerializeObject(memento);

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
				Servers = new Dictionary<string, Uri>();
			}

			public static StorageModelMemento FromModel(IStorageModel model)
			{
				var memento = new StorageModelMemento();
				
				memento.Servers = model.Servers.ToDictionary(p => p.Key, p => p.Value);

				return memento;
			}
		}
	}
}
