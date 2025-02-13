using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace MooshieGames.Common.UI
{
	/// <summary>
	/// A custom UI element for selecting and previewing colors using HSV sliders.
	/// </summary>
	public class ColorPicker : VisualElement, IDisposable
	{
		#region Properties

		/// <summary>
		/// The currently selected color.
		/// </summary>
		public Color Color { get; private set; }

		private float Hue { get => hueSlider?.value ?? 0f; set => hueSlider?.SetValue(value); }
		private float Saturation { get => saturationSlider?.value ?? 0f; set => saturationSlider?.SetValue(value); }
		private float Value { get => valueSlider?.value ?? 0f; set => valueSlider?.SetValue(value); }

		#endregion

		#region UI Elements

		private VisualElement colorPreview;
		private Slider hueSlider;
		private Slider saturationSlider;
		private Slider valueSlider;

		#endregion

		#region Events

		/// <summary>
		/// Event triggered when the color changes.
		/// </summary>
		public event Action<Color> OnColorChanged;

		#endregion

		#region Constructor

		public ColorPicker()
		{
			// Create UI elements
			colorPreview = this.CreateChild("color_picker-color-preview");
			var sliderGroup = this.CreateChild("color_picker-slider-group");

			hueSlider = sliderGroup.CreateChild<Slider>("colorPicker-slider")
				.SetRange(0f, 1f)
				.SetValue(0.5f);

			saturationSlider = sliderGroup.CreateChild<Slider>("colorPicker-slider")
				.SetRange(0f, 1f)
				.SetValue(1f);

			valueSlider = sliderGroup.CreateChild<Slider>("colorPicker-slider")
				.SetRange(0f, 1f)
				.SetValue(1f);

			//Update Color Preview To Reflect Default State
			UpdateColor();

			// Register slider change events
			RegisterSliderEvents();
		}

		public ColorPicker(Color defaultColor)
		{
			// Create UI elements
			colorPreview = this.CreateChild("color_picker-color-preview");
			var sliderGroup = this.CreateChild("color_picker-slider-group");

			hueSlider = sliderGroup.CreateChild<Slider>("colorPicker-slider").SetRange(0f, 1f);
			saturationSlider = sliderGroup.CreateChild<Slider>("colorPicker-slider").SetRange(0f, 1f);
			valueSlider = sliderGroup.CreateChild<Slider>("colorPicker-slider").SetRange(0f, 1f);

			//Update Color Preview To Reflect Default State
			SetColor(defaultColor);

			// Register slider change events
			RegisterSliderEvents();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Sets the color of the ColorPicker and updates its UI.
		/// </summary>
		/// <param name="newColor">The new color to set.</param>
		public void SetColor(Color newColor)
		{
			Color = newColor;
			var h = 0f;
			var s = 0f;
			var v = 0f;
			Color.RGBToHSV(newColor, out h, out s, out v);

			Hue = h;
			Saturation = s;
			Value = v;
			UpdateColor();
		}

		private void UpdateColor()
		{
			Color = Color.HSVToRGB(Hue, Saturation, Value);
			colorPreview.style.backgroundColor = new StyleColor(Color);
			// Invoke the color changed event
			OnColorChanged?.Invoke(Color);
		}

		private void RegisterSliderEvents()
		{
			hueSlider.RegisterValueChangedCallback(_ => UpdateColor());
			saturationSlider.RegisterValueChangedCallback(_ => UpdateColor());
			valueSlider.RegisterValueChangedCallback(_ => UpdateColor());
		}

		#endregion

		#region Disposal

		/// <summary>
		/// Disposes of the ColorPicker by unregistering events.
		/// </summary>
		public void Dispose()
		{
			hueSlider.UnregisterValueChangedCallback(_ => UpdateColor());
			saturationSlider.UnregisterValueChangedCallback(_ => UpdateColor());
			valueSlider.UnregisterValueChangedCallback(_ => UpdateColor());
		}

		#endregion
	}
}