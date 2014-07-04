using System;
using System.Linq;
using Cruise.Commands.Run;
using Cruise.Storage;
using Cruise.Transport;
using NSubstitute;
using Should;
using Xunit;

namespace Tests.Commands.RunCommandTests
{
	public class RunTests : CommandTestBase
	{
		private readonly ITransportModel _transport;
		private readonly IStorageModel _storage;
		private readonly LogResponse _writer;
		private readonly RunCommand _command;

		public RunTests()
		{
			_writer = new LogResponse();

			_storage = Substitute.For<IStorageModel>();
			_storage.Servers.Returns(new[]
			{
				new ServerDetails("Primary", new Uri("http://p.example.com")),
				new ServerDetails("Secondary", new Uri("http://s.example.com")), 
			});

			_transport = Substitute.For<ITransportModel>();

			_transport
				.GetProjects("Primary")
				.Returns(new[] { TestProject, OtherProject });

			_transport
				.GetProjects("Secondary")
				.Returns(new[] { OtherProject });

			_command = new RunCommand(_writer, _storage, _transport);
		}

		[Fact]
		public void When_no_project_name_is_supplied()
		{
			var input = new RunInputModel();

			var result = _command.Execute(input);

			result.ShouldBeFalse();
			_transport.DidNotReceive().TriggerProject(Arg.Any<string>(), Arg.Any<string>());
		}

		[Fact]
		public void When_just_a_project_name_is_supplied_and_it_is_unique()
		{
			var input = new RunInputModel { Project = "Test Project" };

			_command.Execute(input);

			_transport.Received().TriggerProject("Primary", "Test Project");
		}

		[Fact]
		public void When_just_a_project_name_is_supplied_and_it_is_not_unique()
		{
			var input = new RunInputModel { Project = "Other Project" };

			_command.Execute(input);

			_transport.DidNotReceive().TriggerProject(Arg.Any<string>(), Arg.Any<string>());

			_writer.Log.ShouldEqual(new[]
			{
				"Error, Ambiguous Project Name:",
				"Did you mean:",
				"    Primary/Test Project",
				"    Secondary/Test Project",
				"",
			});

		}

		[Fact]
		public void When_a_project_name_is_supplied_and_it_does_not_exist()
		{
			var input = new RunInputModel { Project = "Some Project" };

			_command.Execute(input);

			_transport.DidNotReceive().TriggerProject(Arg.Any<string>(), Arg.Any<string>());

			_writer.Log.ShouldEqual(new[]
			{
				"Error, Unable to find project 'Some Project'.",
				""
			});
		}

		[Fact]
		public void When_a_project_and_server_name_is_supplied_and_it_doesnt_exist()
		{
			var input = new RunInputModel { Project = "Primary/Some Project" };

			_command.Execute(input);

			_transport.DidNotReceive().TriggerProject(Arg.Any<string>(), Arg.Any<string>());

			_writer.Log.ShouldEqual(new[]
			{
				"Error, Unable to find project 'Primary/Some Project'.",
				""
			});
		}

		[Fact]
		public void When_a_project_and_server_name_is_supplied_and_it_exists()
		{
			var input = new RunInputModel { Project = "Primary/Test Project" };

			_command.Execute(input);

			_transport.Received().TriggerProject("Primary", "Test Project");
		}
	}
}
