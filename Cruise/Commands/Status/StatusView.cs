using System;
using System.Linq;
using Cruise.Infrastructure.ViewEngine;
using Cruise.Storage;

namespace Cruise.Commands.Status
{
	public class StatusView : View<StatusViewModel>
	{
		private readonly IConfigurationModel _storage;

		public StatusView(IConfigurationModel storage)
		{
			_storage = storage;
		}

		public override void Render(StatusViewModel model)
		{
			var ordered = model.Servers
				.OrderBy(p => p.Key.Name)
				.ToList();

			ordered.ForEach(detail =>
			{
				Console.WriteLine("{0}:", detail.Key);

				var projects = detail
					.Value
					.OrderBy(p => p.Name)
					.ToList();

				projects.ForEach(project =>
				{

					Console.WriteLine("    {0,-12}{1}", project.Status, project.Name);
				});

				Console.WriteLine();
			});
		}
	}
}
