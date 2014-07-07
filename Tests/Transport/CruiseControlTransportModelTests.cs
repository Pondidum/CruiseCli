using System;
using System.Linq;
using Cruise.Storage;
using Cruise.Transport;
using FubuCore;
using Should;
using Xunit;

namespace Tests.Transport.CruiseControlTransportModelTests
{
	public class CruiseControlTransportModelTestBase
	{
		protected const string ProjectName = "Test";

		protected readonly ServerDetails _server;
		protected readonly CruiseControlTransportModel _transport;

		public CruiseControlTransportModelTestBase()
		{
			_server = new ServerDetails("Local", new Uri("tcp://127.0.0.1:21234/CruiseManager.rem"));
			_transport = new CruiseControlTransportModel();
		}
		
	}

	public class GetProjectsTests : CruiseControlTransportModelTestBase
	{
		[FactDebuggerOnly]
		public void When_there_is_a_server_and_it_has_a_project()
		{
			var projects = _transport.GetProjects(_server);

			projects.ShouldNotBeEmpty();
			projects.Any(p => p.Name.EqualsIgnoreCase(ProjectName)).ShouldBeTrue();
		}
	}

	public class TriggerProjectTests : CruiseControlTransportModelTestBase
	{
		[FactDebuggerOnly]
		public void When_triggering_a_project()
		{
			_transport.TriggerProject(_server, ProjectName);
		}
	}

	public class StartProjectTests : CruiseControlTransportModelTestBase
	{
		[FactDebuggerOnly]
		public void When_starting_a_projects_integrator()
		{
			_transport.StartProject(_server, ProjectName);
		}
	}

	public class StopProjectTests : CruiseControlTransportModelTestBase
	{
		[FactDebuggerOnly]
		public void When_stopping_a_projects_integrator()
		{
			_transport.StopProject(_server, ProjectName);
		}
	}
}
