﻿using System;
using Cruise.Commands.Status;
using Cruise.Storage;
using Cruise.Transport;
using NSubstitute;
using Should;
using Xunit;

namespace Tests.Commands.StatusCommandTests
{
	public class StatusWithServerProjectArgumentTests : CommandTestBase
	{
		private readonly LogResponse _writer;
		private readonly ITransportModel _transport;
		private readonly StatusCommand _command;
		private readonly IServerDetails _primaryServer;
		private readonly IServerDetails _secondaryServer;

		public StatusWithServerProjectArgumentTests()
		{
			_writer = new LogResponse();

			_primaryServer = new ServerDetails("Primary", new Uri("http://p.example.com"));
			_secondaryServer = new ServerDetails("Secondary", new Uri("http://s.example.com"));

			var configuration = new FakeConfigurationModel();
			configuration.Insert(_primaryServer);
			configuration.Insert(_secondaryServer);

			_transport = Substitute.For<ITransportModel>();

			_command = new StatusCommand(_writer, configuration, _transport);
		}

		[Fact]
		public void When_passed_a_server_only()
		{
			var input = new StatusInputModel
			{
				Project = "Primary/"
			};

			_transport
				.GetProjects(_primaryServer)
				.Returns(new[] { TestProject });

			_command.Execute(input);

			var model = _writer.Last<StatusViewModel>();

			model.Servers.Count.ShouldEqual(1);
		}

		[Fact]
		public void When_passed_a_project_only_and_the_project_name_is_unique()
		{
			var input = new StatusInputModel
			{
				Project = "Test Project"
			};

			_transport
				.GetProjects(_primaryServer)
				.Returns(new[] { TestProject });

			_transport
				.GetProjects(_secondaryServer)
				.Returns(new[] { OtherProject });

			_command.Execute(input);

			var model = _writer.Last<StatusViewModel>();

			model.Servers.Count.ShouldEqual(1);
		}

		[Fact]
		public void When_passed_a_project_only_and_the_project_name_is_not_unique()
		{
			var input = new StatusInputModel
			{
				Project = "Test Project"
			};

			_transport
				.GetProjects(_primaryServer)
				.Returns(new[] { TestProject });

			_transport
				.GetProjects(_secondaryServer)
				.Returns(new[] { TestProject });

			_command.Execute(input);

			var model = _writer.Last<StatusViewModel>();

			model.Servers.Count.ShouldEqual(2);
		}

		[Fact]
		public void When_passed_a_server_and_project_name()
		{
			var input = new StatusInputModel
			{
				Project = "Primary/Test Project"
			};

			_transport
				.GetProjects(_primaryServer)
				.Returns(new[] { TestProject });

			_command.Execute(input);
			
			var model = _writer.Last<StatusViewModel>();

			model.Servers.Count.ShouldEqual(1);
		}
	}
}