using System;
using System.Collections.Generic;
using System.Linq;
using Cruise.Commands.Server;
using Cruise.Commands.Server.Actions;
using Cruise.Infrastructure;
using Cruise.Storage;
using NSubstitute;
using Should;
using Xunit;

namespace Tests.Commands.ServerCommandTests
{
	public class ServerListTests
	{
		private readonly ListServerCommandAction _command;
		private readonly FakeConfigurationModel _configuration;
		private readonly LogResponse _writer;
		private readonly ISaveStorageModelCommand _save;

		public ServerListTests()
		{
			_save = Substitute.For<ISaveStorageModelCommand>();
			_configuration = new FakeConfigurationModel();
			_writer = new LogResponse();

			_command = new ListServerCommandAction(_configuration, _writer);
		}

		[Fact]
		public void When_there_are_no_registered_servers()
		{
			_command.Execute(new ServerInputModel());

			_writer.Last<ListServerViewModel>().Servers.ShouldBeEmpty();
		}

		[Fact]
		public void When_there_is_one_registered_server()
		{
			_configuration.Insert(new ServerDetails("test", new Uri("http://example.com")));

			_command.Execute(new ServerInputModel());

			_writer.Last<ListServerViewModel>().Servers.Count.ShouldEqual(1);
		}

		[Fact]
		public void When_there_are_two_registered_servers()
		{
			_configuration.Insert(new ServerDetails("first", new Uri("http://example.com")));
			_configuration.Insert(new ServerDetails("second", new Uri("http://example.com")));

			_command.Execute(new ServerInputModel());

			var names = _writer.Last<ListServerViewModel>().Servers.Select(s => s.Name).ToList();
			
			names.ShouldContain("first");
			names.ShouldContain("second");
		}

		[Fact]
		public void When_the_verbose_flag_is_set()
		{
			_configuration.Insert(new ServerDetails("first", new Uri("http://f.example.com")));
			_configuration.Insert(new ServerDetails("second", new Uri("http://s.example.com")));

			_command.Execute(new ServerInputModel {VerboseFlag = true});

			var names = _writer.Last<ListServerViewModel>().Servers.Select(s => s.Name).ToList();

			names.ShouldContain("first");
			names.ShouldContain("second");
		}
	}
}
