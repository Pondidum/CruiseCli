﻿using System;
using System.IO;
using System.Linq;
using Cruise.Storage;
//using FubuCore;
using NSubstitute;
using Should;
using Xunit;
using Cruise.Infrastructure;

namespace Tests.Storage
{
	public class SaveStorageModelCommandTests
	{
		private readonly IFileSystem _fileSystem;
		private string _contents;

		public SaveStorageModelCommandTests()
		{
			_fileSystem = Substitute.For<IFileSystem>();
			_fileSystem
				.When(f => f.WriteFile(Arg.Any<string>(), Arg.Any<Stream>()))
				.Do(info =>
				{
					var ms = (MemoryStream)info.Arg<Stream>();
					var sr = new StreamReader(ms);
					_contents = sr.ReadToEnd();
				});
		}

		[Fact]
		public void When_saving_a_blank_model()
		{
			var model = Substitute.For<IStorageModel>();
			model.Servers.Returns(Enumerable.Empty<IServerDetails>());

			var cmd = new SaveStorageModelCommand(_fileSystem);
			cmd.Execute(model);

			_fileSystem.Received().WriteFile(Arg.Any<string>(), Arg.Any<Stream>());
			_contents.ShouldNotBeEmpty();
		}

		[Fact]
		public void When_saving_a_populated_model()
		{
			var model = Substitute.For<IStorageModel>();
			model.Servers.Returns(new[] { new ServerDetails("local", new Uri("tcp://127.0.0.1:21234/CruiseManager.rem")) });

			var cmd = new SaveStorageModelCommand(_fileSystem);
			cmd.Execute(model);

			_fileSystem.Received().WriteFile(Arg.Any<string>(), Arg.Any<Stream>());
			_contents.ShouldNotBeEmpty();
		}
	}
}
