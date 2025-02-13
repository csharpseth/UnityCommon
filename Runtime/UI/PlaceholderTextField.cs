using MooshieGames.Common;
using UnityEngine;
using UnityEngine.UIElements;

namespace MooshieGames.Common.UI
{
	public class PlaceholderTextField : TextField
	{
		private readonly Label placeholderLabel;

		public PlaceholderTextField()
		{
			// Create TextField
			this.RegisterValueChangedCallback(OnTextChanged);

			// Create Placeholder Label
			placeholderLabel = new Label("placeholder")
			{
				pickingMode = PickingMode.Ignore // Ensure the label does not intercept clicks
			};


			placeholderLabel.AddToClassList("placeholder");
			placeholderLabel.style.position = Position.Absolute;
			placeholderLabel.style.unityTextAlign = TextAnchor.MiddleLeft;
			ElementAt(0).Add(placeholderLabel);
			placeholderLabel.BringToFront();

			// Register for DetachFromPanelEvent to clean up
			RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);
		}

		public PlaceholderTextField SetPlaceholderText(string text)
		{
			placeholderLabel.text = text;
			return this;
		}

		public PlaceholderTextField SetText(string text)
		{
			value = text;
			return this;
		}

		public PlaceholderTextField ClearText()
		{
			value = string.Empty;
			return this;
		}

		private void OnTextChanged(ChangeEvent<string> evt)
		{
			if (evt.newValue.Length > 20) SetValueWithoutNotify(evt.previousValue);

			UpdatePlaceholderVisibility();
		}

		private void UpdatePlaceholderVisibility() => placeholderLabel.SetVisible(string.IsNullOrEmpty(value));

		private void OnDetachFromPanel(DetachFromPanelEvent evt)
		{
			// Cleanup: Unregister the value changed callback
			this.UnregisterValueChangedCallback(OnTextChanged);

			// Optionally, unregister this callback itself
			UnregisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);
		}
	}
}