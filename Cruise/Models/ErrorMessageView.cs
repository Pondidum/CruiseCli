using System;
using Cruise.Infrastructure.ViewEngine;

namespace Cruise.Models
{
	public class ErrorMessageView : View<ErrorMessageViewModel>
	{
		public override void Render(ErrorMessageViewModel model)
		{
			Console.WriteLine(model.Message);
			Console.WriteLine();
		}
	}
}
