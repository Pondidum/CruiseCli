using System;
using System.Collections.Generic;
using System.Linq;
using Cruise.Commands.Server;
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
		private readonly FakeStorageModel _storage;
		private readonly LogResponse _writer;
		private readonly ISaveStorageModelCommand _save;

		public ServerListTests()
		{
			_save = Substitute.For<ISaveStorageModelCommand>();
			_storage = new FakeStorageModel();
			_writer = new LogResponse();

			_command = new ListServerCommandAction(_storage, _writer);
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
			_storage.Insert(new ServerDetails("test", new Uri("http://example.com")));

			_command.Execute(new ServerInputModel());

			_writer.Last<ListServerViewModel>().Servers.Count.ShouldEqual(1);
		}

		[Fact]
		public void When_there_are_two_registered_servers()
		{
			_storage.Insert(new ServerDetails("first", new Uri("http://example.com")));
			_storage.Insert(new ServerDetails("second", new Uri("http://example.com")));

			_command.Execute(new ServerInputModel());

			var names = _writer.Last<ListServerViewModel>().Servers.Select(s => s.Name).ToList();
			
			names.ShouldContain("first");
			names.ShouldContain("second");
		}

		[Fact]
		public void When_the_verbose_flag_is_set()
		{
			_storage.Insert(new ServerDetails("first", new Uri("http://f.example.com")));
			_storage.Insert(new ServerDetails("second", new Uri("http://s.example.com")));

			_command.Execute(new ServerInputModel {VerboseFlag = true});

			var names = _writer.Last<ListServerViewModel>().Servers.Select(s => s.Name).ToList();

			names.ShouldContain("first");
			names.ShouldContain("second");
		}
	}
}
