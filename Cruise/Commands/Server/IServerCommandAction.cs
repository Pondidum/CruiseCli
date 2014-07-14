namespace Cruise.Commands.Server
{
	public interface IServerCommandAction
	{
		bool CanHandle(ServerInputModel input);
		bool Execute(ServerInputModel input);
	}
}