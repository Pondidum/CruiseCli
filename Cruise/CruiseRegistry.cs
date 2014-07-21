using Cruise.Commands.Server;
using Cruise.Infrastructure;
using Cruise.Infrastructure.ViewEngine;
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
				a.AddAllTypesOf<View>();
			});


			For<IConfigurationModel>()
				.Use(x => x.GetInstance<GetStorageModelQuery>().Execute())
				.Singleton();

			For<IConfigStore>()
				.Use<UserProfileConfigStore>();

			For<IResponseWriter>()
				.Use<ViewResponseWriter>();

			For<ITransportModel>()
				.Use<CruiseControlTransportModel>();
		}
	}
}
