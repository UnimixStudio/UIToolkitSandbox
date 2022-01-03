using UnityEditor;

using UnityEngine.UIElements;

public abstract class DefaultPropertyDrawerByVisualTree : PropertyDrawer
{
	protected abstract VisualTreeAsset VisualTreeAsset { get; }

	public override VisualElement CreatePropertyGUI(SerializedProperty property)
	{
		var root = new VisualElement();

		string treAssetPath = "Assets/Scripts/Editor/ListViewExample/Resources/CountItemProperty.uxml";
		var visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(treAssetPath);

		VisualTreeAsset.CloneTree(root);

		return root;
	}
}