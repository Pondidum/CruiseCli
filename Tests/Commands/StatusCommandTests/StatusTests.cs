using System;
using System.Linq;
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
		private readonly IStorageModel _storage;
		private readonly ITransportModel _transport;
		private readonly StatusCommand _command;
		private readonly IServerDetails _testServer;
		private readonly IServerDetails _secondServer;

		public StatusTests()
		{
			_testServer = new ServerDetails("Test", new Uri("http://example.com"));
			_secondServer = new ServerDetails("Second", new Uri("http://example.com"));

			_writer = new LogResponse();
			_storage = Substitute.For<IStorageModel>();
			_transport = Substitute.For<ITransportModel>();

			_command = new StatusCommand(_writer, _storage, _transport);
		}

		[Fact]
		public void When_there_are_no_servers()
		{
			_storage.Servers.Returns(Enumerable.Empty<ServerDetails>());

			_command.Execute(new StatusInputModel());

			_transport.DidNotReceive().GetProjects(Arg.Any<IServerDetails>());
			_writer.Log.ShouldBeEmpty();
		}

		[Fact]
		public void When_there_is_one_server_with_no_projects()
		{
			_storage.Servers.Returns(new[] { _testServer });
			_transport.GetProjects(_testServer).Returns(Enumerable.Empty<IProject>());

			_command.Execute(new StatusInputModel());

			_writer.Log.ShouldEqual(new[]
			{
				"Test:",
				""
			});
		}

		[Fact]
		public void When_there_is_one_server_with_one_project()
		{
			_storage.Servers.Returns(new[] { _testServer });

			_transport
				.GetProjects(_testServer)
				.Returns(new[] { TestProject });

			_command.Execute(new StatusInputModel());

			_writer.Log.ShouldEqual(new[]
			{
				"Test:",
				"    Success     Test Project",
				 ""
			});
		}

		[Fact]
		public void When_there_are_two_servers_with_projects()
		{
			_storage.Servers.Returns(new[] { _testServer, _secondServer });

			_transport.GetProjects(_testServer).Returns(new[] { TestProject });
			_transport.GetProjects(_secondServer).Returns(new[] { TestProject });

			_command.Execute(new StatusInputModel());

			_writer.Log.ShouldEqual(new[]
			{
				"Second:",
				"    Success     Test Project",
				 "",
				 "Test:",
				"    Success     Test Project",
				 ""
			});
		}
	}
}
