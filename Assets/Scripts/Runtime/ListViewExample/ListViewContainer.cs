using UnityEngine;

namespace ListViewExample
{
	[CreateAssetMenu]
	public class ListViewContainer : ScriptableObject
	{
		[SerializeField] private InventoryItem _item;

		[ContextMenu(nameof(AddCountProperty))]
		private void AddCountProperty()
		{
			_item.AddCountProperty();
		}
		[ContextMenu(nameof(AddColorProperty))]
		private void AddColorProperty()
		{
			_item.AddColorProperty();
		}
	}
}