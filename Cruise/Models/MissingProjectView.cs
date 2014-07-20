using System;
using Cruise.Infrastructure.ViewEngine;

namespace Cruise.Models
{
	public class MissingProjectView : View<MissingProjectViewModel>
	{
		public override void Render(MissingProjectViewModel model)
		{
			Console.WriteLine("Error, unable to find project '{0}'.", model.Project);
		}
	}
}
