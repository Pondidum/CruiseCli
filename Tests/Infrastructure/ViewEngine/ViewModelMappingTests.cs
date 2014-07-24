using System;
using System.Linq;
using Cruise.Infrastructure.ViewEngine;
using FubuCore;
using Xunit;

namespace Tests.Infrastructure.ViewEngine
{
	public class ViewModelMappingTests
	{
		[Fact]
		public void All_models_have_a_view()
		{
			var types = typeof (View<>)
				.Assembly
				.GetTypes();

			var models = types
				.Where(t => t.Name.EndsWith("ViewModel", StringComparison.OrdinalIgnoreCase))
				.ToList();

			var views = types
				.Where(t => t.IsConcreteTypeOf<View>())
				.Select(t => new { Type = t, Generic = t.GetMethod("Render").GetParameters().First().ParameterType })
				.ToList();

			foreach (var model in models)
			{
				Assert.True(
					views.Any(v => v.Generic == model), 
					string.Format("There is no view to render '{0}'", model.FullName));
			}	
		} 
	}
}
