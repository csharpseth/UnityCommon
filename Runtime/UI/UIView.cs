using UnityEngine.UIElements;

namespace MooshieGames.Common.UI
{
	public abstract class UIView<T> : UIView where T : UIModel
	{
		protected T model;

		public static T1 CreateFromController<T1>(UIController controller, T model, VisualElement root) where T1 : UIView<T>, new() => new()
		{
			controller = controller,
			model = model,
			root = root
		};
	}

	public abstract class UIView
	{
		protected UIController controller;
		protected VisualElement root;

		public void Build()
		{
			root?.Clear();
			BuildView();
			controller.OnViewBuilt();
		}

		protected abstract void BuildView();

		public static T CreateFromController<T>(UIController controller, VisualElement root) where T : UIView, new() => new()
		{
			controller = controller,
			root = root
		};
	}
}