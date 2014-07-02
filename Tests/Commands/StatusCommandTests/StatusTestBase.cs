using Cruise.Transport;
using NSubstitute;

namespace Tests.Commands.StatusCommandTests
{
	public class StatusTestBase
	{
		protected IProject NewProject(string name, string status)
		{
			var project = Substitute.For<IProject>();
			project.Name.Returns(name);
			project.Status.Returns(status);

			return project;
		}
	}
}
