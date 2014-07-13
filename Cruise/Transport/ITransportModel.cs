using System.Collections.Generic;
using Cruise.Storage;

namespace Cruise.Transport
{
	public interface ITransportModel
	{
		IEnumerable<IProject> GetProjects(IServerDetails server);

		void StartProject(IServerDetails server, string projectName);
		void StopProject(IServerDetails server, string projectName);
		void TriggerProject(IServerDetails server, string projectName);

		void VolunteerToFixProject(IServerDetails server, string projectName, string userName);
	}
}
