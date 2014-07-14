using System.Collections.Generic;
using System.Linq;

namespace Cruise.Commands.Server
{
	public class ServerCommandActionFactory
	{
		private readonly List<IServerCommandAction> _actions;

		public ServerCommandActionFactory(IEnumerable<IServerCommandAction> actions)
		{
			_actions = actions.ToList();
		}

		public bool Execute(ServerInputModel inputModel)
		{
			return _actions
				.FirstOrDefault(a => a.CanHandle(inputModel))
				.Execute(inputModel);
		}
	}
}
