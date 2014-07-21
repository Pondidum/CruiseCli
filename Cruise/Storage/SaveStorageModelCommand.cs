using System.IO;
using Cruise.Infrastructure;
using Newtonsoft.Json;

namespace Cruise.Storage
{
	public class SaveStorageModelCommand : ISaveStorageModelCommand
	{
		private readonly IConfigStore _configuration;

		public SaveStorageModelCommand(IConfigStore configuration)
		{
			_configuration = configuration;
		}

		public void Execute(IConfigurationModel model)
		{
			var memento = model.ToMemento();

			var json = JsonConvert.SerializeObject(memento);

			using (var stream = new MemoryStream())
			using (var writer = new StreamWriter(stream))
			{
				writer.Write(json);
				writer.Flush();

				stream.Position = 0;

				_configuration.Write(stream);
			}
		}
	}
}
