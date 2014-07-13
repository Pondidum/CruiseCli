using FubuCore.CommandLine;

namespace Cruise.Commands.Volenteer
{
	public class VolenteerCommand : FubuCommand<VolenteerInputModel>
	{
		public VolenteerCommand()
		{
			Usage("Volenteers to fix a server/project")
				.Arguments(a => a.Project);
		}

		public override bool Execute(VolenteerInputModel input)
		{
			throw new System.NotImplementedException();
		}
	}
}
