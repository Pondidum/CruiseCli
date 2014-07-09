using System.IO;
using Cruise.Infrastructure;

namespace Tests
{
	public class InMemoryConfigStore : IConfigStore
	{
		private string _contents;

		public void Write(Stream contents)
		{
			using (var sr = new StreamReader(contents))
			{
				_contents = sr.ReadToEnd();
			}
		}

		public Stream Read()
		{
			var ms = new MemoryStream();
			var writer = new StreamWriter(ms);

			writer.Write(_contents);

			return ms;
		}
	}
}
