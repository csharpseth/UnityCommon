namespace MooshieGames.Common.UI
{
	public abstract class UIModel
	{
		protected UIController controller;
		public abstract void Load();

		public static T CreateFromController<T>(UIController controller) where T : UIModel, new() => new()
		{
			controller = controller
		};
	}
}