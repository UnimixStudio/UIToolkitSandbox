using System;

using UnityEditor;

using UnityEngine;
using UnityEngine.UIElements;

namespace UIElementsExamples
{
	public class QuickTool : EditorWindow
	{
		[MenuItem("QuickTool/Open _%#T")]
		public static void ShowWindow()
		{
			// Opens the window, otherwise focuses it if it’s already open.
			var window = GetWindow<QuickTool>();

			// Adds a title to the window.
			window.titleContent = new GUIContent("QuickTool");

			// Sets a minimum size to the window.
			window.minSize = new Vector2(250, 50);
		}

		private void OnEnable()
		{
			// Reference to the root of the window.
			VisualElement root = rootVisualElement;
		
			// Loads and clones our VisualTree (eg. our UXML structure) inside the root.
			var quickToolVisualTree = Resources.Load<VisualTreeAsset>("QuickTool_Main");
			quickToolVisualTree.CloneTree(root);

			// Queries all the buttons (via type) in our root and passes them
			// in the SetupButton method.
			UQueryBuilder<Button> toolButtons = root.Query<Button>();
			toolButtons.ForEach(SetupButton);
		}

		private void SetupButton(Button button)
		{
			// Reference to the VisualElement inside the button that serves
			// as the button’s icon.
			VisualElement buttonIcon = button.Q(className: "quicktool-button-icon");

			// Icon’s path in our project.
			string iconPath = $"Icons/{button.parent.name}_icon";

			// Loads the actual asset from the above path.
			var iconAsset = Resources.Load<Texture2D>(iconPath);

			// Applies the above asset as a background image for the icon.
			buttonIcon.style.backgroundImage = iconAsset;

			// Instantiates our primitive object on a left click.
			button.clickable.clicked += () => CreateObject(button.parent.name);

			// Sets a basic tooltip to the button itself.
			button.tooltip = button.parent.name;
		}

		private void CreateObject(string primitiveTypeName)
		{
			var pt = (PrimitiveType)Enum.Parse(typeof(PrimitiveType), primitiveTypeName, true);
			GameObject go = ObjectFactory.CreatePrimitive(pt);
			go.transform.position = Vector3.zero;
		}
	}
}