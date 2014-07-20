using System;
using Cruise.Infrastructure.ViewEngine;

namespace Cruise.Models
{
	public class AmbiguousProjectNameView : View<AmbiguousProjectNameViewModel>
	{
		public override void Render(AmbiguousProjectNameViewModel model)
		{
			Console.WriteLine("Error, ambiguous Project name.");
			Console.WriteLine("Did you mean:");

			model.PossibleProjects.ForEach(project => Console.WriteLine("    {0}", project));

			Console.WriteLine();
		}
	}
}
