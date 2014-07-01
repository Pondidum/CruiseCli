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
	public class StatusTests
	{
		private readonly LogResponse _writer;
		private readonly IStorageModel _storage;
		private readonly ITransportModel _transport;
		private readonly StatusCommand _command;

		public StatusTests()
		{
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

			_transport.DidNotReceive().GetServer(Arg.Any<string>());
			_writer.Log.ShouldBeEmpty();
		}

		[Fact]
		public void When_there_is_one_server_with_no_projects()
		{
			_storage.Servers.Returns(new[] { new ServerDetails("Test", new Uri("http://example.com")) });
			_transport.GetServer("Test").Returns(Substitute.For<IServer>());

			_command.Execute(new StatusInputModel());

			_transport.Received().GetServer("Test");

			var expected = new[] { "Test:", "" }.ToList();

			_writer.Log.ShouldEqual(expected);
		}

		[Fact]
		public void When_there_is_one_server_with_one_project()
		{
			_storage.Servers.Returns(new[] { new ServerDetails("Test", new Uri("http://example.com")) });

			var project = Substitute.For<IProject>();
			project.Name.Returns("Test Project");
			project.Status.Returns("Success");

			var server = Substitute.For<IServer>();
			server.Projects.Returns(new[] { project });

			_transport.GetServer("Test").Returns(server);

			_command.Execute(new StatusInputModel());

			_transport.Received().GetServer("Test");

			var expected = new[]
			{
				"Test:",
				"    Success     Test Project",
				 ""
			}.ToList();

			_writer.Log.ShouldEqual(expected);
		}

		[Fact]
		public void When_there_are_two_servers_with_projects()
		{

		}
	}
}
