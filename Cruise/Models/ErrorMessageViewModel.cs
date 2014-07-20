namespace Cruise.Models
{
	public class ErrorMessageViewModel
	{
		public string Message { get; private set; }

		public ErrorMessageViewModel(string message)
		{
			Message = message;
		}
	}
}
