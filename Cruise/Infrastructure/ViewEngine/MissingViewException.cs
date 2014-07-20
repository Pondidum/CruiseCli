using System;

namespace Cruise.Infrastructure.ViewEngine
{
	public class MissingViewException : Exception
	{
		public MissingViewException(Type modelType) 
			: base(string.Format("Unable to find a view for '{0}'.", modelType.FullName))
		{
		}
	}
}
