using Cruise.Infrastructure;
using Cruise.Storage;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Cruise
{
	public class CruiseRegistry : Registry
	{
		public CruiseRegistry()
		{
			Scan(a =>
			{
				a.TheCallingAssembly();
				a.WithDefaultConventions();
			});

			For<IStorageModel>()
				.Use(x => x.GetInstance<StorageController>().Load())
				.Singleton();

			For<IResponse>().Use<ConsoleResponse>();
		}
	}
}
