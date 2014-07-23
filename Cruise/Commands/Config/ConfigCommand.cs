using FubuCore.CommandLine;

namespace Cruise.Commands.Config
{
	public class ConfigCommand : FubuCommand<ConfigInputModel>
	{
		public ConfigCommand()
		{
			Usage("List color configuration")
				.ValidFlags(f => f.ColorFlag);

			Usage("View color value for a category")
				.Arguments(a => a.Category)
				.ValidFlags(f => f.ColorFlag);
		}

		public override bool Execute(ConfigInputModel input)
		{
			throw new System.NotImplementedException();
		}
	}
}
