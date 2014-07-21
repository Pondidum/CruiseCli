namespace Cruise.Storage
{
	public interface ISaveStorageModelCommand
	{
		void Execute(IConfigurationModel model);
	}
}
