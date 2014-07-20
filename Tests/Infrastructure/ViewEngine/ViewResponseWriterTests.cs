using System;
using System.Linq;
using Cruise.Infrastructure.ViewEngine;
using NSubstitute;
using Should;
using Xunit;

namespace Tests.Infrastructure.ViewEngine
{
	public class ViewResponseWriterTests
	{
		[Fact]
		public void When_no_view_is_registered_for_model_type()
		{
			var engine = new ViewResponseWriter(Enumerable.Empty<View>());

			Assert.Throws<MissingViewException>(() => engine.Write(new TestViewModel { Name = "Dave" }));
		}

		[Fact]
		public void When_a_view_can_handle_a_model()
		{
			var view = Substitute.For<View<TestViewModel>>();
			var model = new TestViewModel { Name = "Dave" };

			var engine = new ViewResponseWriter(new[] { view });
			engine.Write(model);

			view.Received().Render(model);
		}

		public class TestViewModel
		{
			public string Name { get; set; }
		}

		public class TestView : View<TestViewModel>
		{
			private readonly Action<string> _writer;

			public TestView(Action<string> writer)
			{
				_writer = writer;
			}

			public override void Render(TestViewModel model)
			{
				_writer.Invoke(model.Name);
			}
		}
	}
}
