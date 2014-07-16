using System;
using Cruise;
using Cruise.Infrastructure;
using Cruise.Storage;
using Cruise.Transport;
using NSubstitute;
using Should;
using Xunit;

namespace Tests
{
	public class SingleProjectActionTests
	{
		private readonly ITransportModel _transport;
		private readonly SingleProjectAction _action;

		public SingleProjectActionTests()
		{
			var localServer = new ServerDetails("local", new Uri("tcp://local.server:21234/CruiseManager.rem"));
			var testProject = NewProject("Test", "Good");

			var writer = Substitute.For<IResponse>();

			var storage = new FakeStorageModel();
			storage.Insert(localServer);

			_transport = Substitute.For<ITransportModel>();
			_transport.GetProjects(localServer).Returns(new[] { testProject });

			_action = new SingleProjectAction(writer, storage, _transport);
		}

		[Fact]
		public void It_fixes_the_casing_of_the_project_name()
		{
			_action.Action = (s, p) => p.ShouldEqual("Test");

			_action.Execute(new ProjectName("local", "test"));
		}

		private IProject NewProject(string name, string status)
		{
			var project = Substitute.For<IProject>();
			project.Name.Returns(name);
			project.Status.Returns(status);

			return project;
		}
	}
}
