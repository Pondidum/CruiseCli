using System.Collections.Generic;
using System.Linq;

namespace Cruise.Models
{
	public class AmbiguousProjectNameViewModel
	{
		public List<ProjectName> PossibleProjects { get; private set; }

		public AmbiguousProjectNameViewModel(IEnumerable<ProjectName> possibleProjects )
		{
			PossibleProjects = possibleProjects.ToList();
		}
	}
}
