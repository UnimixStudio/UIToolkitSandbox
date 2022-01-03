using UnityEditor;

using UnityEngine.UIElements;

namespace ListViewExample
{
	[CustomPropertyDrawer(typeof(ColorItemProperty))]
	public class ColorItemPropertyPropertyDrawer : DefaultPropertyDrawerByVisualTree
	{
		protected override VisualTreeAsset VisualTreeAsset =>
			AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
				"Assets/Scripts/Editor/ListViewExample/Resources/ColorItemProperty.uxml");
	}
}