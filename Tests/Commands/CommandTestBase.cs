using Cruise.Transport;
using NSubstitute;

namespace Tests.Commands
{
	public class CommandTestBase
	{
		protected IProject TestProject;
		protected IProject OtherProject;

		public CommandTestBase()
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
