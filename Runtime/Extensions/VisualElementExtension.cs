using UnityEngine;
using UnityEngine.UIElements;

namespace MooshieGames.Common
{
	public static class VisualElementExtensions
	{
		/// <summary>
		/// Sets the visibility of a VisualElement by toggling its display style.
		/// </summary>
		/// <param name="element">The VisualElement to modify.</param>
		/// <param name="isVisible">True to make the element visible; false to hide it.</param>
		/// <remarks>
		/// Adjusts the `style.display` property of the element. When visible, the style is set to `DisplayStyle.Flex`; otherwise, it is set to `DisplayStyle.None`.
		/// </remarks>
		public static VisualElement SetVisible(this VisualElement element, bool isVisible)
		{
			element.style.display = isVisible ? DisplayStyle.Flex : DisplayStyle.None;
			return element;
		}

		/// <summary>
		/// Creates a new child VisualElement, assigns it one or more USS classes, and adds it to a parent element.
		/// </summary>
		/// <param name="parent">The parent VisualElement to which the new child will be added.</param>
		/// <param name="classes">Optional USS class names to apply to the new child element.</param>
		/// <returns>The newly created VisualElement child.</returns>
		public static VisualElement CreateChild(this VisualElement parent, params string[] classes)
		{
			var child = new VisualElement();
			child.AddClass(classes).AddTo(parent);
			return child;
		}

		/// <summary>
		/// Creates a new child VisualElement of a specified type, assigns it one or more USS classes, and adds it to a parent element.
		/// </summary>
		/// <typeparam name="T">The type of the VisualElement to create (must be a subclass of VisualElement).</typeparam>
		/// <param name="parent">The parent VisualElement to which the new child will be added.</param>
		/// <param name="classes">Optional USS class names to apply to the new child element.</param>
		/// <returns>The newly created child element of type <typeparamref name="T"/>.</returns>
		public static T CreateChild<T>(this VisualElement parent, params string[] classes) where T : VisualElement, new()
		{
			var child = new T();
			child.AddClass(classes).AddTo(parent);
			return child;
		}

		/// <summary>
		/// Adds a VisualElement to a specified parent element.
		/// </summary>
		/// <typeparam name="T">The type of the VisualElement being added (must be a subclass of VisualElement).</typeparam>
		/// <param name="child">The VisualElement to add.</param>
		/// <param name="parent">The parent VisualElement to which the child will be added.</param>
		/// <returns>The child VisualElement, for method chaining.</returns>
		public static T AddTo<T>(this T child, VisualElement parent) where T : VisualElement
		{
			parent.Add(child);
			return child;
		}

		/// <summary>
		/// Adds one or more USS classes to a VisualElement.
		/// </summary>
		/// <typeparam name="T">The type of the VisualElement being modified (must be a subclass of VisualElement).</typeparam>
		/// <param name="visualElement">The VisualElement to which the USS classes will be added.</param>
		/// <param name="classes">An array of USS class names to add.</param>
		/// <returns>The modified VisualElement, for method chaining.</returns>
		/// <remarks>
		/// Skips empty or null class names in the provided array.
		/// </remarks>
		public static T AddClass<T>(this T visualElement, params string[] classes) where T : VisualElement
		{
			foreach (var cls in classes)
			{
				if (string.IsNullOrEmpty(cls)) continue;

				visualElement.AddToClassList(cls);
			}

			return visualElement;
		}

		public static T SetValue<T>(this T slider, float value) where T : Slider
		{
			slider.value = value;
			return slider;
		}

		public static T SetValue<T>(this T slider, int value) where T : SliderInt
		{
			slider.value = value;
			return slider;
		}

		public static T SetRange<T>(this T slider, float min, float max) where T : Slider
		{
			slider.lowValue = min;
			slider.highValue = max;
			return slider;
		}

		public static T SetRange<T>(this T slider, int min, int max) where T : SliderInt
		{
			slider.lowValue = min;
			slider.highValue = max;
			return slider;
		}

		public static T SetText<T>(this T element, string value) where T : TextElement
		{
			element.text = value;
			return element;
		}

		public static T ClearText<T>(this T element) where T : TextElement
		{
			element.text = string.Empty;
			return element;
		}

		public static T SetImage<T>(this T element, Texture2D image) where T : VisualElement
		{
			element.style.backgroundImage = image;
			return element;
		}

		public static T SetName<T>(this T element, string name) where T : VisualElement
		{
			element.name = name;
			return element;
		}
	}
}