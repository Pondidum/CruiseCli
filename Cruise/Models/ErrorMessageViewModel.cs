using System.Text;

namespace Cruise.Models
{
	public class ErrorMessageViewModel
	{
		private readonly string _message;

		public ErrorMessageViewModel(string message)
		{
			_message = message;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine(_message);
			sb.AppendLine();

			return sb.ToString();
		}
	}
}
