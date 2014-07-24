using Cruise.Commands.Config;
using Cruise.Infrastructure;
using Cruise.Models;
using Cruise.Storage;
using NSubstitute;
using Xunit;

namespace Tests.Commands.ConfigCommandTests
{
	public class ConfigColorCategoryTests : CommandTestBase
	{
		private IConfigurationModel _config;
		private IResponseWriter _writer;
		private ConfigCommand _command;

		public ConfigColorCategoryTests()
		{
			_writer = Substitute.For<IResponseWriter>();
			_config = new FakeConfigurationModel();

			_command = new ConfigCommand(_writer, _config);
		}

		[Fact]
		public void When_a_valid_category_is_specified()
		{
			_command.Execute(new ConfigInputModel
			{
				ColorFlag = true,
				Category = "Error",
			});

			_writer.Received().Write(Arg.Any<ConfigColorListViewModel>());
		}

		[Fact]
		public void When_a_valid_category_case_differing_is_specified()
		{
			_command.Execute(new ConfigInputModel
			{
				ColorFlag = true,
				Category = "ERROR",
			});

			_writer.Received().Write(Arg.Any<ConfigColorListViewModel>());
		}

		[Fact]
		public void When_an_invalid_category_is_specified()
		{

			_command.Execute(new ConfigInputModel
			{
				ColorFlag = true,
				Category = "TESTOMG",
			});

			_writer.Received().Write(Arg.Any<ErrorMessageViewModel>());
		}
	}
}
