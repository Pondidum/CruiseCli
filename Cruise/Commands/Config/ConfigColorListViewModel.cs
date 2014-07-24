using System.Collections.Generic;

namespace Cruise.Commands.Config
{
	public class ConfigColorListViewModel
	{
		public Dictionary<string, string> ColorMap { get; set; }

		public ConfigColorListViewModel()
		{
			ColorMap = new Dictionary<string, string>();
		}
	}
}
