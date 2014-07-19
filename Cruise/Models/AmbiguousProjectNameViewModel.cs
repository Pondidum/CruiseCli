using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cruise.Models
{
	public class AmbiguousProjectNameViewModel
	{
		private readonly List<ProjectName> _possibleProjects;

		public AmbiguousProjectNameViewModel(IEnumerable<ProjectName> possibleProjects )
		{
			_possibleProjects = possibleProjects.ToList();
		}

		public override string ToString()
		{
			var sb = new StringBuilder();

			sb.AppendLine("Error, ambiguous Project name.");
			sb.AppendLine("Did you mean:");

			_possibleProjects.ForEach(project => sb.AppendFormat("    {0}", project).AppendLine());

			sb.AppendLine();

			return sb.ToString();
		}
	}
}
