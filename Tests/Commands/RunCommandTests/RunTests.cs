using System;
using System.Linq;
using Cruise;
using Cruise.Commands.Run;
using Cruise.Models;
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
		private readonly LogResponse _writer;
		private readonly RunCommand _command;
		private readonly IServerDetails _primaryServer;
		private readonly IServerDetails _secondaryServer;

		public RunTests()
		{
			_writer = new LogResponse();

			_primaryServer = new ServerDetails("Primary", new Uri("http://p.example.com"));
			_secondaryServer = new ServerDetails("Secondary", new Uri("http://s.example.com"));

			var configuration = new FakeConfigurationModel();
			configuration.Insert(_primaryServer);
			configuration.Insert(_secondaryServer);

			_transport = Substitute.For<ITransportModel>();

			_transport
				.GetProjects(_primaryServer)
				.Returns(new[] { TestProject, OtherProject });

			_transport
				.GetProjects(_secondaryServer)
				.Returns(new[] { OtherProject });

			var action = new SingleProjectAction(_writer, configuration, _transport);

			_command = new RunCommand(_transport, action);
		}

		[Fact]
		public void When_no_project_name_is_supplied()
		{
			var input = new RunInputModel();

			var result = _command.Execute(input);

			result.ShouldBeFalse();
			_transport.DidNotReceive().TriggerProject(Arg.Any<IServerDetails>(), Arg.Any<string>());
		}

		[Fact]
		public void When_just_a_server_name_is_supplied()
		{
			var input = new RunInputModel { Project = "Primary/" };

			_command.Execute(input);

			_transport.DidNotReceive().TriggerProject(Arg.Any<IServerDetails>(), Arg.Any<string>());
			_writer.LastModel.ShouldBeType<ErrorMessageViewModel>();
		}

		[Fact]
		public void When_just_a_project_name_is_supplied_and_it_is_unique()
		{
			var input = new RunInputModel { Project = "Test Project" };

			_command.Execute(input);

			_transport.Received().TriggerProject(_primaryServer, "Test Project");
		}

		[Fact]
		public void When_just_a_project_name_is_supplied_and_it_is_not_unique()
		{
			var input = new RunInputModel { Project = "Other Project" };

			_command.Execute(input);

			_transport.DidNotReceive().TriggerProject(Arg.Any<IServerDetails>(), Arg.Any<string>());

			_writer.LastModel.ShouldBeType<AmbiguousProjectNameViewModel>();
		}

		[Fact]
		public void When_just_a_project_name_is_supplied_and_it_does_not_exist()
		{
			var input = new RunInputModel { Project = "Some Project" };

			_command.Execute(input);

			_transport.DidNotReceive().TriggerProject(Arg.Any<IServerDetails>(), Arg.Any<string>());

			_writer.LastModel.ShouldBeType<MissingProjectViewModel>();
		}

		[Fact]
		public void When_a_project_and_server_name_is_supplied_and_it_doesnt_exist()
		{
			var input = new RunInputModel { Project = "Primary/Some Project" };

			_command.Execute(input);

			_transport.DidNotReceive().TriggerProject(Arg.Any<IServerDetails>(), Arg.Any<string>());

			_writer.LastModel.ShouldBeType<MissingProjectViewModel>();
		}

		[Fact]
		public void When_a_project_and_server_name_is_supplied_and_it_exists()
		{
			var input = new RunInputModel { Project = "Primary/Test Project" };

			_command.Execute(input);

			_transport.Received().TriggerProject(_primaryServer, "Test Project");
		}
	}
}
