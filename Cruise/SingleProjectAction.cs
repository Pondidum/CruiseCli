using System;
using System.Collections.Generic;
using System.Linq;
using Cruise.Infrastructure;
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
				_writer.Write("Error, you must specify a Project name.");
				_writer.Write("");

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
				_writer.Write("Error, unable to find project '{0}'.", spec);
				_writer.Write("");
				return false;
			}

			if (serverDetails.Count > 1)
			{
				_writer.Write("Error, ambiguous Project name.");
				_writer.Write("Did you mean:");

				serverDetails
					.Select(detail => new ProjectName(detail.Key.Name, spec.Project))
					.Each(detail => _writer.Write("    {0}", detail));
				_writer.Write("");

				return false;
			}

			var server = serverDetails.First();
			var project = server.Value.First();

			Action.Invoke(server.Key, project.Name);

			return true;
		}
	}
}