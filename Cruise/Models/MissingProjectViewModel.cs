namespace Cruise.Models
{
	public class MissingProjectViewModel
	{
		public ProjectName Project { get; private set; }

		public MissingProjectViewModel(ProjectName project)
		{
			Project = project;
		}
	}
}
