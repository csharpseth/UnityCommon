using UnityEngine;
using UnityEngine.UIElements;

namespace MooshieGames.Common.UI
{
	public abstract class UIController<T1, T2> : UIController<T1> where T1 : UIView<T2>, new() where T2 : UIModel, new()
	{
		private T2 model;

		protected override void Setup()
		{
			if (styleSheet)
				document.rootVisualElement.styleSheets.Add(styleSheet);

			model = new T2();
			model.Load();
			Cleanup();
			view = UIView<T2>.CreateFromController<T1>(this, model, document.rootVisualElement);
			view.Build();
		}
	}

	public abstract class UIController<T> : UIController where T : UIView, new()
	{
		protected T view;
		protected bool hasInitialized;

		protected virtual void SetVisible(bool visible)
		{
			if (view == null) return;

			document.rootVisualElement.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;
			if (visible)
			{
				document.rootVisualElement.BringToFront();
			}
		}

		protected virtual void Close() => SetVisible(false);

		public override void OnViewBuilt()
		{
			if (hasInitialized) return;

			SetVisible(!startHidden);
			Init();
			hasInitialized = true;
		}

		protected virtual void Cleanup()
		{
			hasInitialized = false;
		}

		protected virtual void Setup()
		{
			if (styleSheet)
				document.rootVisualElement?.styleSheets.Add(styleSheet);

			view = UIView.CreateFromController<T>(this, document.rootVisualElement);
			view.Build();
		}

		private void Start() => Setup();
		private void OnDisable() => Cleanup();
	}

	[RequireComponent(typeof(UIDocument))]
	public abstract class UIController : MonoBehaviour
	{
		public StyleSheet styleSheet;
		public UIDocument document;
		public bool startHidden = true;

		/// <summary>
		/// Called by the view once the coroutine has completed.
		/// </summary>
		public virtual void OnViewBuilt()
		{
		}

		protected abstract void Init();
	}
}