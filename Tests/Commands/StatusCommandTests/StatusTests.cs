using System;
using System.Linq;
using Cruise.Commands.Server;
using Cruise.Commands.Status;
using Cruise.Infrastructure;
using Cruise.Storage;
using Cruise.Transport;
using NSubstitute;
using Should;
using Xunit;

namespace Tests.Commands.StatusCommandTests
{
	public class StatusTests : CommandTestBase
	{
		private readonly LogResponse _writer;
		private readonly FakeConfigurationModel _storage;
		private readonly ITransportModel _transport;
		private readonly StatusCommand _command;
		private readonly IServerDetails _testServer;
		private readonly IServerDetails _secondServer;

		public StatusTests()
		{
			_testServer = new ServerDetails("Test", new Uri("http://example.com"));
			_secondServer = new ServerDetails("Second", new Uri("http://example.com"));

			_writer = new LogResponse();
			_storage = new FakeConfigurationModel();
			_transport = Substitute.For<ITransportModel>();

			_command = new StatusCommand(_writer, _storage, _transport);
		}

		[Fact]
		public void When_there_are_no_servers()
		{
			_command.Execute(new StatusInputModel());

			_transport.DidNotReceive().GetProjects(Arg.Any<IServerDetails>());
			
			_writer.Last<StatusViewModel>().Servers.ShouldBeEmpty();
		}

		[Fact]
		public void When_there_is_one_server_with_no_projects()
		{
			_storage.Insert(_testServer);
			_transport.GetProjects(_testServer).Returns(Enumerable.Empty<IProject>());

			_command.Execute(new StatusInputModel());

			var model = _writer.Last<StatusViewModel>();
			
			model.Servers.Count.ShouldEqual(1);
			model.Servers.First().Value.ShouldBeEmpty();
		}

		[Fact]
		public void When_there_is_one_server_with_one_project()
		{
			_storage.Insert(_testServer);

			_transport
				.GetProjects(_testServer)
				.Returns(new[] { TestProject });

			_command.Execute(new StatusInputModel());

			var model = _writer.Last<StatusViewModel>();

			model.Servers.Count.ShouldEqual(1);
			model.Servers.First().Value.Count.ShouldEqual(1);
		}

		[Fact]
		public void When_there_are_two_servers_with_projects()
		{
			_storage.Insert(_testServer);
			_storage.Insert(_secondServer);

			_transport.GetProjects(_testServer).Returns(new[] { TestProject });
			_transport.GetProjects(_secondServer).Returns(new[] { TestProject });

			_command.Execute(new StatusInputModel());

			var model = _writer.Last<StatusViewModel>();

			model.Servers.Count.ShouldEqual(2);
			model.Servers.First().Value.Count.ShouldEqual(1);
			model.Servers.Last().Value.Count.ShouldEqual(1);
		}
	}
}
