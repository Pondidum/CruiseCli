using System;
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
		private readonly IStorageModel _storage;
		private readonly ITransportModel _transport;
		private readonly StatusCommand _command;

		public StatusWithServerProjectArgumentTests()
		{
			_writer = new LogResponse();
			_storage = Substitute.For<IStorageModel>();
			_transport = Substitute.For<ITransportModel>();

			_command = new StatusCommand(_writer, _storage, _transport);


			_storage.Servers.Returns(new[]
			{
				new ServerDetails("Primary", new Uri("http://p.example.com")),
				new ServerDetails("Secondary", new Uri("http://s.example.com"))
			});

		}

		[Fact]
		public void When_passed_a_server_only()
		{
			var input = new StatusInputModel
			{
				Project = "Primary/"
			};

			_transport
				.GetProjects("Primary")
				.Returns(new[] { TestProject});

			_command.Execute(input);

			_writer.Log.ShouldEqual(new[]
			{
				"Primary:", 
				"    Success     Test Project", 
				""
			});
		}

		[Fact]
		public void When_passed_a_project_only_and_the_project_name_is_unique()
		{
			var input = new StatusInputModel
			{
				Project = "Test Project"
			};

			_transport
				.GetProjects("Primary")
				.Returns(new[] { TestProject });

			_transport
				.GetProjects("Secondary")
				.Returns(new[] { OtherProject });

			_command.Execute(input);

			_writer.Log.ShouldEqual(new[]
			{
				"Primary:", 
				"    Success     Test Project", 
				""
			});
		}

		[Fact]
		public void When_passed_a_project_only_and_the_project_name_is_not_unique()
		{
			var input = new StatusInputModel
			{
				Project = "Test Project"
			};

			_transport
				.GetProjects("Primary")
				.Returns(new[] { TestProject });

			_transport
				.GetProjects("Secondary")
				.Returns(new[] { TestProject });

			_command.Execute(input);

			_writer.Log.ShouldEqual(new[]
			{
				"Primary:", 
				"    Success     Test Project", 
				"",
				"Secondary:", 
				"    Success     Test Project", 
				""
			});
		}

		[Fact]
		public void When_passed_a_server_and_project_name()
		{
			var input = new StatusInputModel
			{
				Project = "Primary/Test Project"
			};

			_transport
				.GetProjects("Primary")
				.Returns(new[] { TestProject });

			_command.Execute(input);

			_writer.Log.ShouldEqual(new[]
			{
				"Primary:", 
				"    Success     Test Project", 
				""
			});
		}
	}
}