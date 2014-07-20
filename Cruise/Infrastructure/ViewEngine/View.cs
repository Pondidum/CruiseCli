namespace Cruise.Infrastructure.ViewEngine
{

	public abstract class View
	{}

	public abstract class View<TModel> : View
	{
		public abstract void Render(TModel model);
	}
}
