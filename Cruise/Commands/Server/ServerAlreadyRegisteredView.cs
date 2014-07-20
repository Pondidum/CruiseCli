using System;
using Cruise.Infrastructure.ViewEngine;

namespace Cruise.Commands.Server
{
	public class ServerAlreadyRegisteredView : View<ServerAlreadyRegisteredViewModel>
	{
		public override void Render(ServerAlreadyRegisteredViewModel model)
		{
			Console.WriteLine(
				"Server {0} ({1}) is already registered.", 
				model.Name,
				model.Url);
		}
	}
}
