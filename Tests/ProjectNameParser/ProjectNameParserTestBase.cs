using Cruise;

namespace Tests
{
	public class ProjectNameParserTestBase
	{
		public ProjectName Result { get; private set; }

		protected void Execute(string name)
		{
			var parser = new ProjectNameParser();

			Result = parser.Parse(name);
		}
	}
}
