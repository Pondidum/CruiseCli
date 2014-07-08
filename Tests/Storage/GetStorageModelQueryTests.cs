using System;
using System.IO;
using System.Linq;
using Cruise.Infrastructure;
using Cruise.Storage;
using Newtonsoft.Json;
using NSubstitute;
using Should;
using Xunit;

namespace Tests.Storage
{
	public class GetStorageModelQueryTests
	{
		private readonly IConfigStore _configuration;

		public GetStorageModelQueryTests()
		{
			_configuration = Substitute.For<IConfigStore>();
		}

		[Fact]
		public void When_the_file_exists_but_is_empty()
		{
			_configuration.Read().Returns(new MemoryStream());

			var query = new GetStorageModelQuery(_configuration);
			var model = query.Execute();

			model.Servers.ShouldBeEmpty();
		}

		[Fact]
		public void When_the_file_exists_but_is_invalid()
		{
			using(var ms = new MemoryStream())
			using (var sw = new StreamWriter(ms))
			{
				sw.WriteLine("erwefwfwewefwf");
				sw.Flush();
				ms.Position = 0;

				_configuration.Read().Returns(ms);

				var query = new GetStorageModelQuery(_configuration);

				Assert.Throws<JsonReaderException>(() => query.Execute());
			}
		}

		[Fact]
		public void When_the_file_exists_and_is_valid()
		{
			using (var ms = new MemoryStream())
			using (var sw = new StreamWriter(ms))
			{
				sw.WriteLine("{\"Servers\":{\"local\":\"tcp://127.0.0.1:21234/CruiseManager.rem\"}}");
				sw.Flush();
				ms.Position = 0;

				_configuration.Read().Returns(ms);

				var query = new GetStorageModelQuery(_configuration);
				var model = query.Execute();

				var server = model.Servers.First();

				server.Name.ShouldEqual("local");
				server.Url.ShouldEqual(new Uri("tcp://127.0.0.1:21234/CruiseManager.rem"));
			}
		}
	}
}
