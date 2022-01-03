using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Codice.CM.Common.Serialization;

using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;

using Object = UnityEngine.Object;

namespace ListViewExample
{
	[CustomPropertyDrawer(typeof(InventoryItem))]
	public class InventoryItemPropertyDrawer : PropertyDrawer
	{
		private readonly List<object> _sourceItems = new List<object>();
		private ListView _listView;

		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			var root = new VisualElement();

			var treeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
				"Assets/Scripts/Editor/ListViewExample/Resources/InvenotryItem.uxml");
			treeAsset.CloneTree(root);

			SerializedProperty properties = property.FindPropertyRelative("_properties");

			foreach(SerializedProperty prop in properties)
			{
				_sourceItems.Add(prop.managedReferenceValue);
			}

			if (_listView == null)
			{
				_listView = root.Q<ListView>("Properties");
				Debug.Log($"{nameof(_listView)} = {_listView}");
				Debug.Log($"{nameof(_listView.itemsSource)} = {_listView.itemsSource}");

				_listView.itemsSource = _sourceItems;

				_listView.makeItem = () => new PropertyField(properties.GetEndProperty());

				_listView.itemsRemoved += ints => { _sourceItems.RemoveAt(ints.First()); };

				_listView.itemsAdded += indexes =>
				{
					int index = indexes.ToArray()[0];

					string interfaceName = properties.type;
					string baseTypeName = interfaceName.Substring(1, interfaceName.Length - 1);

					var genericMenu = new GenericMenu();

					foreach(Type type in GetTypes(interfaceName))
					{
						string item = type.Name.Replace(baseTypeName, "");
						SerializedProperty arrayElementAtIndex = properties.GetArrayElementAtIndex(index);

						AddMenuItem(genericMenu, type, item, arrayElementAtIndex);
					}

					genericMenu.ShowAsContext();
				};
			}

			ConfigButton(properties, root);

			return root;
		}

		private static IEnumerable<Type> GetTypes(string interfaceName)
		{
			IEnumerable<Type> allTypesOfCurrentDomain =
				AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes());

			IEnumerable<Type> typesOfCurrentDomain =
				allTypesOfCurrentDomain as Type[] ?? allTypesOfCurrentDomain.ToArray();

			Type typeOfPropertyElement = typesOfCurrentDomain.First(type => type.Name == interfaceName);

			return typesOfCurrentDomain.Where(
				type => type != typeOfPropertyElement && typeOfPropertyElement.IsAssignableFrom(type));
		}

		private void AddMenuItem(GenericMenu genericMenu, Type type, string item, SerializedProperty property)
		{
			void AddNewElement(object data)
			{
				var elementType = (Type)data;

				object instance = Activator.CreateInstance(elementType);

				property.managedReferenceValue = instance;
				property.isExpanded = true;
				property.serializedObject.ApplyModifiedProperties();

				_listView.Rebuild();
			}

			genericMenu.AddItem(new GUIContent(item), false, AddNewElement, type);
		}

		private void ConfigButton(SerializedProperty properties, VisualElement root)
		{
			var button = new Button(LogAllValues) { text = nameof(LogAllValues) };

			void LogAllValues()
			{
				int index = 0;
				foreach(object o in properties)
				{
					Debug.Log($"---{index}---");

					var serializedProperty = o as SerializedProperty;
					Debug.Log($"managedReferenceFieldTypename : {serializedProperty.managedReferenceFieldTypename}");
					Debug.Log($"managedReferenceValue : {serializedProperty.managedReferenceValue}");
					Debug.Log($"boxedValue : {serializedProperty.boxedValue}");
					index++;

					Debug.Log("---------");
				}

				index = 0;
				foreach(object o in _sourceItems)
				{
					Debug.Log($"---{index}---");

					Debug.Log(o);
					index++;

					Debug.Log("---------");
				}
			}

			root.Add(button);
		}
	}
}