using System.Text;

namespace Cruise.Models
{
	public class MissingProjectViewModel
	{
		private readonly ProjectName _project;

		public MissingProjectViewModel(ProjectName project)
		{
			_project = project;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();

			sb.AppendFormat("Error, unable to find project '{0}'.", _project);
			sb.AppendLine();

			return sb.ToString();
		}
	}
}
