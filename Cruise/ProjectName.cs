namespace Cruise
{
	public class ProjectName
	{
		public string Server { get; private set; }
		public string Project { get; private set; }

		public ProjectName(string server, string project)
		{
			Server = server;
			Project = project;
		}
	}
}
