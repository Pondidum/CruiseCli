using System;
using FubuCore.CommandLine;
using StructureMap;

namespace Cruise
{
	public class StructureMapAdaptor : ICommandCreator
	{
		private readonly IContainer _container;

		public StructureMapAdaptor(IContainer container)
		{
			_container = container;
		}

		public IFubuCommand Create(Type commandType)
		{
			return (IFubuCommand)_container.GetInstance(commandType);
		}
	}
}
