using UnityEngine.UIElements;

namespace MooshieGames.Common.UI
{
	/// <summary>
	/// A custom integer slider with a dynamic label that updates to show the defined label text and the current value.
	/// </summary>
	public class DisplayIntSlider : SliderInt
	{
		private string labelText;

		/// <summary>
		/// Creates a new DisplayIntSlider with the specified parameters.
		/// </summary>
		/// <param name="labelText">The text to display before the slider value.</param>
		/// <param name="min">The minimum value of the slider.</param>
		/// <param name="max">The maximum value of the slider.</param>
		/// <param name="initialValue">The initial value of the slider.</param>
		public DisplayIntSlider()
		{
			// Update the label to show the initial value
			UpdateLabelText();

			// Register callback to update the label when the slider value changes
			this.RegisterValueChangedCallback(OnValueChanged);
			// Register cleanup callback for when the element is removed from the UI hierarchy
			RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);
		}

		public DisplayIntSlider SetLabel(string labelText)
		{
			this.labelText = labelText;
			UpdateLabelText();
			return this;
		}

		/// <summary>
		/// Updates the label text to reflect the current value and label text prefix.
		/// </summary>
		private void UpdateLabelText() => label = $"{labelText}{value}";

		/// <summary>
		/// Callback invoked when the slider value changes.
		/// </summary>
		/// <param name="evt">The event data.</param>
		private void OnValueChanged(ChangeEvent<int> evt) => UpdateLabelText();

		/// <summary>
		/// Cleans up event registrations when the element is removed from the panel.
		/// </summary>
		/// <param name="evt">The event data.</param>
		private void OnDetachFromPanel(DetachFromPanelEvent evt)
		{
			// Unregister value changed callback
			this.UnregisterValueChangedCallback(OnValueChanged);
			// Unregister this cleanup callback
			UnregisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);
		}
	}
}