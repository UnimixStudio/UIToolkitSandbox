using UnityEditor;

namespace ListViewExample
{
	[CustomEditor(typeof(ListViewContainer))]
	[CanEditMultipleObjects]
	public class ListViewContainerCustomEditor : DefaultEditorDrawer { }
}