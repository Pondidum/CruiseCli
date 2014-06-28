using System;
using Cruise.Storage;
using Xunit;

namespace Tests.Storage
{
	public class StorageModelTests
	{
		[Fact]
		public void Servers_cannot_be_registered_twice()
		{
			var model = new StorageModel(new StorageController.StorageModelMemento());

			model.Register("test", new Uri("http://example.com"));

			Assert.Throws<ServerAlreadyRegisteredException>(() => model.Register("test", new Uri("http://example.com")));
		}
	}
}
