using System;
using System.Linq;
using Cruise.Storage;
using Should;
using Xunit;

namespace Tests.Storage
{
	public class StorageModelTests
	{
		private readonly IStorageModel _model;

		public StorageModelTests()
		{
			_model = new StorageModel(new StorageController.StorageModelMemento());

			_model.Register("test", new Uri("http://example.com"));
		}

		[Fact]
		public void Servers_cannot_be_registered_twice()
		{
			Assert.Throws<ServerAlreadyRegisteredException>(() => _model.Register("test", new Uri("http://example.com")));
		}

		[Fact]
		public void Server_names_are_not_case_sensitive()
		{
			_model.IsRegistered("TEST").ShouldBeTrue();
			_model.Servers.Count().ShouldEqual(1);
		}

		[Fact]
		public void Servers_can_be_unregistered()
		{
			_model.UnRegister("test");
			
			_model.Servers.ShouldBeEmpty();
		}

		[Fact]
		public void Servers_can_be_unregistered_case_insensitive()
		{
			_model.UnRegister("TEST");

			_model.Servers.ShouldBeEmpty();
		}
	}
}
