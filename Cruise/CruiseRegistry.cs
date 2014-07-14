using Cruise.Commands.Server;
using Cruise.Infrastructure;
using Cruise.Storage;
using Cruise.Transport;
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

				a.AddAllTypesOf<IServerCommandAction>();
			});


			For<IStorageModel>()
				.Use(x => x.GetInstance<GetStorageModelQuery>().Execute())
				.Singleton();

			For<IConfigStore>()
				.Use<UserProfileConfigStore>();

			For<IResponse>()
				.Use<ConsoleResponse>();

			For<ITransportModel>()
				.Use<CruiseControlTransportModel>();
		}
	}
}
