namespace Cruise
{
	public class ProjectNameParser
	{
		public ProjectName Parse(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return new ProjectName(string.Empty, string.Empty);
			}

			var server = string.Empty;
			var project = input;

			if (input.Contains("/"))
			{
				var index = input.IndexOf('/');

				server = input.Substring(0, index);
				project = input.Substring(index + 1);
			}

			return new ProjectName(server, project);
		}
		
	}
}
