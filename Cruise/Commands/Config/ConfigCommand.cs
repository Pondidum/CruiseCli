using System;
using System.Linq;
using Cruise.Infrastructure;
using Cruise.Models;
using Cruise.Storage;
using FubuCore;
using FubuCore.CommandLine;

namespace Cruise.Commands.Config
{
	public class ConfigCommand : FubuCommand<ConfigInputModel>
	{
		private readonly IResponseWriter _writer;
		private readonly IConfigurationModel _config;

		public ConfigCommand(IResponseWriter writer, IConfigurationModel config)
		{
			_writer = writer;
			_config = config;
			Usage("List color configuration")
				.ValidFlags(f => f.ColorFlag);

			Usage("View color value for a category")
				.Arguments(a => a.Category)
				.ValidFlags(f => f.ColorFlag);

			Usage("Set a category color")
				.Arguments(a => a.Category, a => a.Color)
				.ValidFlags(f => f.ColorFlag);
		}

		public override bool Execute(ConfigInputModel input)
		{
			if (input.ColorFlag == false)
			{
				return false;
			}

			var property = _config
				.Colors
				.GetType()
				.GetProperties()
				.FirstOrDefault(p => p.Name.EqualsIgnoreCase(input.Category));

			if (property == null)
			{
				_writer.Write(new ErrorMessageViewModel(string.Format("{0} is not a valid color category.", input.Category)));
				return false;
			}

			var get = property.GetGetMethod();
			var value = (ConsoleColor)get.Invoke(_config.Colors, new object[] {});


			var model = new ConfigColorListViewModel();
			model.ColorMap[property.Name] = value.ToString();

			_writer.Write(model);
			return true;

		}
	}
}
