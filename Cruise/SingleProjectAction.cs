using System;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using Cruise.Infrastructure;
using Cruise.Models;
using Cruise.Storage;
using Cruise.Transport;
using FubuCore;

namespace Cruise
{
	public class SingleProjectAction
	{
		private readonly IResponseWriter _writer;
		private readonly IStorageModel _storage;
		private readonly ITransportModel _transport;

		public SingleProjectAction(IResponseWriter writer, IStorageModel storage, ITransportModel transport)
		{
			_writer = writer;
			_storage = storage;
			_transport = transport;
		}

		public Action<IServerDetails, string> Action { get; set; }

		public bool Execute(ProjectName spec)
		{

			if (spec.IsBlank || spec.HasProject == false)
			{
				_writer.Write(new ErrorMessageViewModel("Error, you must specify a Project name."));

				return false;
			}

			var allServers = _storage.Servers;

			if (spec.HasServer)
			{
				allServers = new[] { _storage.GetServerByName(spec.Server) };
			}

			var serverDetails = allServers
				.ToDictionary(s => s, s => _transport.GetProjects(s))
				.Where(pair => pair.Value.Any(p => p.Name.EqualsIgnoreCase(spec.Project)))
				.ToList();
				

			if (serverDetails.Any() == false)
			{
				_writer.Write(new MissingProjectViewModel(spec));
				return false;
			}

			if (serverDetails.Count > 1)
			{
				var possible = serverDetails.Select(detail => new ProjectName(detail.Key.Name, spec.Project));

				_writer.Write(new AmbiguousProjectNameViewModel(possible));

				return false;
			}

			var server = serverDetails.First();
			var project = server.Value.First();

			Action.Invoke(server.Key, project.Name);

			return true;
		}
	}
}
