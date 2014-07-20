using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cruise.Infrastructure.ViewEngine
{
	public class ViewResponseWriter : IResponseWriter
	{
		private readonly Dictionary<View, MethodInfo> _views;

		public ViewResponseWriter(IEnumerable<View> views)
		{
			_views = views.ToDictionary(
				view => view, 
				view => view.GetType().GetMethod("Render"));
		}

		public void Write(object model)
		{
			var modelType = model.GetType();
			var views = _views
				.Where(v => v.Value.GetParameters().Count() == 1)
				.Where(v => v.Value.GetParameters().First().ParameterType == modelType)
				.ToList();

			if (views.Any() == false)
			{
				throw new MissingViewException(modelType);
			}

			var pair = views.First();
			var view = pair.Key;
			var method = pair.Value;

			method.Invoke(view, new[] { model });
		}
	}
}
