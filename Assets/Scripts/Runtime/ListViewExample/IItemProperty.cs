using System;

using UnityEngine;

namespace ListViewExample
{
	public interface IItemProperty
	{
		string Name { get; }
	}

	[Serializable]
	public class DependencyItems : IItemProperty
	{
		public string Name { get; }
		[SerializeField] private InventoryItem[] _items;
	}

	[Serializable]
	public class CountItemProperty : IItemProperty
	{
		[SerializeField] private string _name;
		public string Name => _name;
		public int Count;
	}

	[Serializable]
	public class ColorItemProperty : IItemProperty
	{
		[SerializeField] private string _name;
		public string Name => _name;
		public Color Color = Color.white;
	}
}