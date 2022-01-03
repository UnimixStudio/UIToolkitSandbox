using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;

namespace UIElementsExamples
{
	public class TextFiledBindToNameOfGameObjectBindingExample : EditorWindow
	{
		private TextField _objectNameBinding;

		[MenuItem("Window/UIElementsExamples/Simple Binding Example")]
		public static void ShowDefaultWindow()
		{
			var wnd = GetWindow<TextFiledBindToNameOfGameObjectBindingExample>();
			wnd.titleContent = new GUIContent("Simple Binding");
		}

		public void OnEnable()
		{
			VisualElement root = base.rootVisualElement;

			_objectNameBinding = new TextField("Object Name Binding")
			{
				bindingPath = "m_Name"
			};

			root.Add(_objectNameBinding);

			OnSelectionChange();
		}

		public void OnSelectionChange()
		{
			var selectedObject = Selection.activeObject as GameObject;
			if (selectedObject == null)
			{
				// Unbind the object from the actual visual element
				//rootVisualElement.Unbind();
				_objectNameBinding.Unbind();

				// Clear the TextField after the binding is removed
				_objectNameBinding.value = "";
			}
			else
			{
				// Create serialization object
				var so = new SerializedObject(selectedObject);
				// Bind it to the root of the hierarchy. It will find the right object to bind to...
				// rootVisualElement.Bind(so);

				// ... or alternatively you can also bind it to the TextField itself.
				_objectNameBinding.Bind(so);
			}
		}
	}
}