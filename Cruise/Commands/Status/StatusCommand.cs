using FubuCore.CommandLine;

namespace Cruise.Commands.Status
{
	public class StatusCommand : FubuCommand<StatusInputModel>
	{
		public override bool Execute(StatusInputModel input)
		{
			return true;
		}
	}
}