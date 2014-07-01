using System.Collections.Generic;

namespace Cruise.Transport
{
	public interface ITransportModel
	{
		IEnumerable<IProject> GetProjects(string serverName);

		void StartProject(string serverName, string projectName);
		void StopProject(string serverName, string projectName);
		void TriggerProject(string serverName, string projectName);
	}
}
