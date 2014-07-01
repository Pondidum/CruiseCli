using System;

namespace Cruise.Storage
{
	public interface IServerDetails
	{
		string Name { get; }
		Uri Url { get; }
	}
}
