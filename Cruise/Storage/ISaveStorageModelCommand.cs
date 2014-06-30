namespace Cruise.Storage
{
	public interface ISaveStorageModelCommand
	{
		void Execute(IStorageModel model);
	}
}
