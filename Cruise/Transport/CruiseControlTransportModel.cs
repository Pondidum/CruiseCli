using System;
using System.Collections.Generic;
using System.Linq;
using Cruise.Storage;
using FubuCore.Util;
using ThoughtWorks.CruiseControl.Remote;

namespace Cruise.Transport
{
	public class CruiseControlTransportModel : ITransportModel
	{
		private readonly Cache<Uri, CruiseServerClientBase> _clients;

		public CruiseControlTransportModel()
		{
			var factory = new CruiseServerClientFactory();
			_clients = new Cache<Uri, CruiseServerClientBase>();

			_clients.OnMissing = url => factory.GenerateClient(url.ToString());
		}

		public IEnumerable<IProject> GetProjects(IServerDetails server)
		{
			var client = _clients[server.Url];
			
			return client
				.GetProjectStatus()
				.Select(p => new Project(p))
				.ToList();
		}

		public void StartProject(IServerDetails server, string projectName)
		{
			_clients[server.Url]
				.StartProject(projectName);
		}

		public void StopProject(IServerDetails server, string projectName)
		{
			_clients[server.Url]
				.StopProject(projectName);
		}

		public void TriggerProject(IServerDetails server, string projectName)
		{
			_clients[server.Url]
				.ForceBuild(projectName);
		}
	}
}
