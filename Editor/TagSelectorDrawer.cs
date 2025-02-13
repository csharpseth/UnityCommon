using MooshieGames.Common;
using UnityEditor;
using UnityEngine;

namespace MooshieGames.Editor
{
	[CustomPropertyDrawer(typeof(TagSelectorAttribute))]
	public class TagSelectorDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType == SerializedPropertyType.String)
			{
				EditorGUI.BeginProperty(position, label, property);
				property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
				EditorGUI.EndProperty();
			}
			else
			{
				EditorGUI.LabelField(position, label.text, "Use [TagSelector] with strings.");
			}
		}
	}
}