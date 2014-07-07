using ThoughtWorks.CruiseControl.Remote;

namespace Cruise.Transport
{
	public class Project : IProject
	{
		private readonly ProjectStatus _projectStatus;

		public Project(ProjectStatus projectStatus)
		{
			_projectStatus = projectStatus;
		}

		public string Name { get { return _projectStatus.Name; } }
		public string Status { get { return _projectStatus.BuildStatus.ToString(); } }
	}
}
