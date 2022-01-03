using UnityEditor;

using UnityEngine.UIElements;

namespace ListViewExample
{
	[CustomPropertyDrawer(typeof(CountItemProperty))]
	public class CountItemPropertyPropertyDrawer : DefaultPropertyDrawerByVisualTree
	{
		protected override VisualTreeAsset VisualTreeAsset =>
			AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
				"Assets/Scripts/Editor/ListViewExample/Resources/CountItemProperty.uxml");
	}
}