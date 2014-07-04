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

			_transport.DidNotReceive().GetProjects(Arg.Any<string>());
			_writer.Log.ShouldBeEmpty();
		}

		[Fact]
		public void When_there_is_one_server_with_no_projects()
		{
			_storage.Servers.Returns(new[] { new ServerDetails("Test", new Uri("http://example.com")) });
			_transport.GetProjects("Test").Returns(Enumerable.Empty<IProject>());

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
			_storage.Servers.Returns(new[]
			{
				new ServerDetails("Test", new Uri("http://example.com"))
			});

			_transport
				.GetProjects("Test")
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
			_storage.Servers.Returns(new[]
			{
				new ServerDetails("Test", new Uri("http://example.com")),
				new ServerDetails("Second", new Uri("http://example.com"))
			});

			_transport.GetProjects("Test").Returns(new[] { TestProject });
			_transport.GetProjects("Second").Returns(new[] { TestProject });

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
