using System.Collections.Generic;

namespace Cruise.Transport
{
	public interface IServer
	{
		IEnumerable<IProject> Projects { get; }

		void StartProject(IProject project);
		void StopProject(IProject project);
		void TriggerProject(IProject project);
	}
}
