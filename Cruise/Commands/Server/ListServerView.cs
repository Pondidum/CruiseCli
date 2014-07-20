using System;
using Cruise.Infrastructure.ViewEngine;

namespace Cruise.Commands.Server
{
	public class ListServerView : View<ListServerViewModel>
	{
		public override void Render(ListServerViewModel model)
		{
			if (model.Verbose)
			{
				model.Servers.ForEach(server => Console.WriteLine("    {0,-12}{1}", server.Name, server.Url));
			}
			else
			{
				model.Servers.ForEach(server => Console.WriteLine("    {0}", server.Name));
			}

		}
	}
}
