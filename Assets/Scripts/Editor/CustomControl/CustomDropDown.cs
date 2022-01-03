using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.UIElements;

public class CustomDropDownWindow : EditorWindow
{
	private CustomDropDown dropDown;

	private void OnEnable()
	{
		var stylesheet =
			AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/CustomControl/CustomDropDownStyles.uss");
		rootVisualElement.styleSheets.Add(stylesheet);
		dropDown = new CustomDropDown();
		rootVisualElement.Add(dropDown);
	}

	[MenuItem("Tools/DropDown")]
	public static void DoIt() =>
		GetWindow<CustomDropDownWindow>().Show();
}

public class CustomDropDown : VisualElement
{
	private VisualElement _menuBackground;

	private List<string> _values = new() { "Value 1", "Value 2", "Value 3" };

	public CustomDropDown()
	{
		AddToClassList("dropdown");

		var title = new TextElement { text = "Click me!", style = { color = Color.black } };
		Add(title);

		IManipulator clickable = new Clickable(ShowMenu);
		this.AddManipulator(clickable);
	}

	private void HideMenu()
	{
		if (_menuBackground == null)
			return;

		_menuBackground.RemoveFromHierarchy();
		_menuBackground = null;
	}

	private void ShowMenu()
	{
		
		if (_menuBackground != null)
			return;

		// Build the menu.

		// We first create a fullscreen (empty) background element that will be used
		//  to capture all clicks outside the menu.  When that occurs,  we simply
		// remove the menu from the hierarchy.

		// Since the menu will be parented on the fullscreen background, we will need
		// to offset it back to its intended position
		Vector3 offset = worldTransform.MultiplyPoint3x4(Vector3.zero);

		// Grab the root element to get its size
		VisualElement root = FindRootElement();
		_menuBackground = new VisualElement
		{
			style =
			{
				position = Position.Absolute,
				left = -offset.x,
				top = -offset.y,
				width = root.style.width,
				height = root.style.height
			}
		};
		IManipulator hideMenuManipulator = new Clickable(HideMenu);
		_menuBackground.AddManipulator(hideMenuManipulator);

		// Create the menu. Align it to the proper offset.
		var menu = new VisualElement
		{
			style =
			{
				position = Position.Absolute,
				left = offset.x - 1, top = offset.y + 20
			}
		};
		menu.AddToClassList("dropdown-menu");
		IManipulator onMenuClickManipulator = new Clickable(OnMenuClick);
		menu.AddManipulator(onMenuClickManipulator);

		foreach(string value in _values)
		{
			var button = new Button(() => Debug.Log(value))
			{
				text = value
			};
			
			button.AddToClassList("dropdown-button");
			
			menu.Add(button);
		}

		_menuBackground.Add(menu);

		Add(_menuBackground);
	}

	private void OnMenuClick() { }

	private VisualElement FindRootElement()
	{
		var root = (VisualElement)this;
		while (root.parent != null)
			root = root.parent;
		return root;
	}
}