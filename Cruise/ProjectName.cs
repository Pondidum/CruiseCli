namespace Cruise
{
	public class ProjectName
	{
		public string Server { get; private set; }
		public string Project { get; private set; }

		public bool HasServer { get; private set; }
		public bool HasProject { get; private set; }

		public bool IsBlank { get; private set; }

		public ProjectName(string server, string project)
		{
			Server = server;
			Project = project;

			HasServer = string.IsNullOrWhiteSpace(Server) == false;
			HasProject = string.IsNullOrWhiteSpace(Project) == false;

			IsBlank = !HasServer && !HasProject;
		}

		public override string ToString()
		{
			if (IsBlank)
			{
				return "<empty>";
			}

			if (HasServer == false)
			{
				return Project;
			}

			if (HasProject == false)
			{
				return Server + "/";
			}

			return Server + "/" + Project;
		}
	}
}
