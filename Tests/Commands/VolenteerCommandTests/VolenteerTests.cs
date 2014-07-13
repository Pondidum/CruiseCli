using System;
using Cruise.Commands.Volenteer;
using Cruise.Storage;
using Cruise.Transport;
using NSubstitute;
using Xunit;

namespace Tests.Commands.VolenteerCommandTests
{
	public class VolenteerTests : CommandTestBase
	{
		private readonly ITransportModel _transport;
		private readonly LogResponse _writer;
		private readonly IServerDetails _primaryServer;
		private readonly IServerDetails _secondaryServer;
		private readonly VolenteerCommand _command;

		public VolenteerTests()
		{
			_writer = new LogResponse();

			_primaryServer = new ServerDetails("Primary", new Uri("http://p.example.com"));
			_secondaryServer = new ServerDetails("Secondary", new Uri("http://s.example.com"));

			var storage = new FakeStorageModel();
			storage.Insert(_primaryServer);
			storage.Insert(_secondaryServer);

			_transport = Substitute.For<ITransportModel>();

			_transport
				.GetProjects(_primaryServer)
				.Returns(new[] { TestProject, OtherProject });

			_transport
				.GetProjects(_secondaryServer)
				.Returns(new[] { OtherProject });

			_command = new VolenteerCommand(_writer, storage, _transport);
		}

		[Fact]
		public void When_no_project_name_is_supplied()
		{
			
		}

		[Fact]
		public void When_just_a_server_name_is_supplied()
		{
			
		}

		[Fact]
		public void When_just_a_project_name_is_supplied_and_it_is_unique()
		{

		}

		[Fact]
		public void When_just_a_project_name_is_supplied_and_it_is_not_unique()
		{

		}

		[Fact]
		public void When_just_a_project_name_is_supplied_and_it_does_not_exist()
		{

		}

		[Fact]
		public void When_a_project_and_server_name_is_supplied_and_it_doesnt_exist()
		{

		}

		[Fact]
		public void When_a_project_and_server_name_is_supplied_and_it_exists()
		{
			
		}
	}
}
