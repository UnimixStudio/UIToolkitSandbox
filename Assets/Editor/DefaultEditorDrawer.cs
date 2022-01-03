using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Object), true, isFallback = true)]
[CanEditMultipleObjects]
public class DefaultEditorDrawer : Editor
{
	public override VisualElement CreateInspectorGUI()
	{
		var root = new VisualElement();
 
		SerializedProperty property = serializedObject.GetIterator();
			
		if (property.NextVisible(true) )
		{
			do
			{
				var field = new PropertyField(property) { name = "PropertyField:" + property.propertyPath };

				if (property.propertyPath == "m_Script" && serializedObject.targetObject != null)
				{
					field.SetEnabled(false);
				}

				root.Add(field);
			} while (property.NextVisible(false));

			return root;
		}

		return root;
	}
}