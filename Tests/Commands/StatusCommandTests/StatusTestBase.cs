using System;
using Cruise.Transport;
using NSubstitute;

namespace Tests.Commands.StatusCommandTests
{
	public class StatusTestBase
	{
		protected IProject TestProject;
		protected IProject OtherProject;

		public StatusTestBase()
		{
			TestProject = NewProject("Test Project", "Success");
			OtherProject = NewProject("Other Project", "Success");
		}

		protected IProject NewProject(string name, string status)
		{
			var project = Substitute.For<IProject>();
			project.Name.Returns(name);
			project.Status.Returns(status);

			return project;
		}
	}
}
