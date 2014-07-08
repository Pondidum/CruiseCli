﻿using System.IO;
using Cruise.Infrastructure;
using Newtonsoft.Json;

namespace Cruise.Storage
{
	public class GetStorageModelQuery
	{
		private readonly IConfigStore _configuration;

		public GetStorageModelQuery(IConfigStore configuration)
		{
			_configuration = configuration;
		}

		public IStorageModel Execute()
		{
			using (var stream = _configuration.Read())
			{
				using (var sr = new StreamReader(stream))
				{
					var memento = JsonConvert.DeserializeObject<StorageModelMemento>(sr.ReadToEnd());

					return new StorageModel(memento ?? new StorageModelMemento());
				}
			}
		}
	}
}
