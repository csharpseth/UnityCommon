using MooshieGames.Common;
using UnityEngine.UIElements;

namespace MooshieGames.Common.UI
{
	public class CustomProgressBar : VisualElement
	{
		private VisualElement _progressVisual;
		private float _lowValue = 0f;
		private float _highValue = 1f;
		private float _value = 0.5f;

		private Length _width = new(0, LengthUnit.Percent);

		public CustomProgressBar()
		{
			_progressVisual = this.CreateChild().SetName("ProgressVisual");
			UpdateVisual();
		}

		public CustomProgressBar SetValue(float value)
		{
			_value = value;
			UpdateVisual();
			return this;
		}

		public CustomProgressBar SetLowValue(float value)
		{
			_lowValue = value;
			UpdateVisual();
			return this;
		}

		public CustomProgressBar SetHighValue(float value)
		{
			_highValue = value;
			UpdateVisual();
			return this;
		}

		private void UpdateVisual()
		{
			_width.value = (_value - _lowValue) / (_highValue - _lowValue) * 100f;
			_progressVisual.style.width = _width;
		}
	}
}