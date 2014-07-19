namespace Cruise.Models
{
	public class GenericModel
	{
		private readonly string _message;
		private readonly object[] _args;

		public GenericModel(string message, params object[] args)
		{
			_message = message;
			_args = args;
		}

		public override string ToString()
		{
			return string.Format(_message, _args);
		}
	}
}
