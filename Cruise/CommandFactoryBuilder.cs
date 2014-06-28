using FubuCore.CommandLine;
using StructureMap;

namespace Cruise
{
	public class CommandFactoryBuilder
	{
		private readonly IContainer _container;

		public CommandFactoryBuilder(IContainer container)
		{
			_container = container;
		}

		public CommandFactory Build()
		{
			var factory = new CommandFactory(new StructureMapAdaptor(_container));

			factory.RegisterCommands(typeof(IFubuCommand).Assembly);
			factory.RegisterCommands(typeof(Program).Assembly);

			return factory;
		}
	}
}
