using Cruise;

namespace Tests.ProjectNameParser
{
	public class ProjectNameParserTestBase
	{
		public ProjectName Result { get; private set; }

		protected void Execute(string name)
		{
			var parser = new Cruise.ProjectNameParser();

			Result = parser.Parse(name);
		}
	}
}
