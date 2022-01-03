using System;
using System.Collections.Generic;

using UnityEngine;

namespace ListViewExample
{
	[Serializable]
	public class InventoryItem
	{
		[SerializeField] private string _name;
		[SerializeField] private int _amount;
		[SerializeReference] private List<IItemProperty> _properties;

		public string Name => _name;

		public int Amount => _amount;

		public void AddCountProperty()
		{
			_properties.Add(new CountItemProperty());
		}
		public void AddColorProperty()
		{
			_properties.Add(new ColorItemProperty());
		}
	}
}