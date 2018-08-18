using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SIS.Items;

namespace SIS.Managers
{
	[CreateAssetMenu(menuName = "Singles/Resources Manager")]
	public class ResourcesManager : ScriptableObject
	{
		public List<Item> allItems = new List<Item>();
		Dictionary<string, Item> itemDict = new Dictionary<string, Item>();

		//Creates data structure linking itemName to Item
		public void Init()
		{
			foreach (Item item in allItems)
			{
				if (!itemDict.ContainsKey(item.name))
					itemDict.Add(item.name, item);
				else
					Debug.Log("There's two items named " + item.name + ". Not allowed.");
			}
		}

		//Creates ScriptableObject, the Item
		public Item InstiateItem(string itemName)
		{
			Item defaultItem = FindItem(itemName);
			if (defaultItem == null)
			{
				Debug.LogWarning("Failed to Find Item Instance: " + itemName);
			}
			Item newItem = Instantiate(defaultItem);
			newItem.name = defaultItem.name;

			return newItem;
		}

		//Finds type from itemDict database
		Item FindItem(string itemName)
		{
			Item retVal = null;
			itemDict.TryGetValue(itemName, out retVal);
			return retVal;
		}
	}
}