using System;
using Cruise;
using Cruise.Commands.Volunteer;
using Cruise.Models;
using Cruise.Storage;
using Cruise.Transport;
using NSubstitute;
using Should;
using Xunit;

namespace Tests.Commands.VolunteerCommandTests
{
	public class VolunteerTests : CommandTestBase
	{
		private readonly ITransportModel _transport;
		private readonly LogResponse _writer;
		private readonly IServerDetails _primaryServer;
		private readonly IServerDetails _secondaryServer;
		private readonly VolunteerCommand _command;

		public VolunteerTests()
		{
			_writer = new LogResponse();

			_primaryServer = new ServerDetails("Primary", new Uri("http://p.example.com"));
			_secondaryServer = new ServerDetails("Secondary", new Uri("http://s.example.com"));

			var storage = new FakeConfigurationModel();
			storage.Insert(_primaryServer);
			storage.Insert(_secondaryServer);

			_transport = Substitute.For<ITransportModel>();

			_transport
				.GetProjects(_primaryServer)
				.Returns(new[] { TestProject, OtherProject });

			_transport
				.GetProjects(_secondaryServer)
				.Returns(new[] { OtherProject });

			var action = new SingleProjectAction(_writer, storage, _transport);
			_command = new VolunteerCommand(_transport, action);
		}

		[Fact]
		public void When_no_project_name_is_supplied()
		{
			var input = new VolunteerInputModel();

			var result = _command.Execute(input);

			result.ShouldBeFalse();
			_transport
				.DidNotReceive()
				.VolunteerToFixProject(Arg.Any<IServerDetails>(), Arg.Any<string>(), Arg.Any<string>());
		}

		[Fact]
		public void When_just_a_server_name_is_supplied()
		{
			var input = new VolunteerInputModel { Project = "Primary/" };

			_command.Execute(input);

			_transport
				.DidNotReceive()
				.VolunteerToFixProject(Arg.Any<IServerDetails>(), Arg.Any<string>(), Arg.Any<string>());


			_writer.LastModel.ShouldBeType<ErrorMessageViewModel>();

		}

		[Fact]
		public void When_just_a_project_name_is_supplied_and_it_is_unique()
		{
			var input = new VolunteerInputModel { Project = "Test Project" };

			_command.Execute(input);

			_transport
				.Received()
				.VolunteerToFixProject(_primaryServer, "Test Project", Environment.UserName);
		}

		[Fact]
		public void When_just_a_project_name_is_supplied_and_it_is_not_unique()
		{
			var input = new VolunteerInputModel { Project = "Other Project" };

			_command.Execute(input);

			_transport
				.DidNotReceive()
				.VolunteerToFixProject(Arg.Any<IServerDetails>(), Arg.Any<string>(), Arg.Any<string>());

			_writer.LastModel.ShouldBeType<AmbiguousProjectNameViewModel>();
		}

		[Fact]
		public void When_just_a_project_name_is_supplied_and_it_does_not_exist()
		{
			var input = new VolunteerInputModel { Project = "Some Project" };

			_command.Execute(input);

			_transport
				.DidNotReceive()
				.VolunteerToFixProject(Arg.Any<IServerDetails>(), Arg.Any<string>(), Arg.Any<string>());

			_writer.LastModel.ShouldBeType<MissingProjectViewModel>();
		}

		[Fact]
		public void When_a_project_and_server_name_is_supplied_and_it_doesnt_exist()
		{
			var input = new VolunteerInputModel { Project = "Primary/Some Project" };

			_command.Execute(input);

			_transport
				.DidNotReceive()
				.VolunteerToFixProject(Arg.Any<IServerDetails>(), Arg.Any<string>(), Arg.Any<string>());

			_writer.LastModel.ShouldBeType<MissingProjectViewModel>();
		}

		[Fact]
		public void When_a_project_and_server_name_is_supplied_and_it_exists()
		{
			var input = new VolunteerInputModel { Project = "Primary/Test Project" };

			_command.Execute(input);

			_transport
				.Received()
				.VolunteerToFixProject(_primaryServer, "Test Project", Environment.UserName);
		}
	}
}
