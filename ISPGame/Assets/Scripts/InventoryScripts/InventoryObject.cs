using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

	[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
	
	public string savePath;
	public ItemDatabaseObject database;
    public List<InventorySlot> Container = new List<InventorySlot>();
	
	private void OnEnable()
	{
#if UNITY_EDITOR
		database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
#else
		database = Resources.Load<ItemDatabaseObject>("Database");
#endif
	}

	public void AddItem(ItemObject _item, int _amount)
	{
		for(int i = 0; i < Container.Count; i++)
		{
			if(Container[i].item == _item)
			{
				Container[i].AddAmount(_amount);
				return;
			}
		}
		
		Container.Add(new InventorySlot(database.GetId[_item], _item, _amount));
	}
	
	public void Save()
	{
		string saveData = JsonUtility.ToJson(this, true);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + savePath);
		bf.Serialize(file, saveData);
		file.Close();
	}
	
	public void Load()
	{
		if(File.Exists(Application.persistentDataPath + savePath))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open((Application.persistentDataPath + savePath), FileMode.Open);
			JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
			file.Close();
		}
	}
	
	public void OnAfterDeserialize()
	{
		for(int i = 0; i < Container.Count; i++)
		{
			Container[i].item = database.GetItem[Container[i].ID];
		}
	}
	
	
	public void OnBeforeSerialize()
	{
	}
	
}

	[System.Serializable]
public class InventorySlot
{
	public int ID { get; } 
	public ItemObject item;
	public int amount { get; set; }
	public InventorySlot(int _id, ItemObject _item, int _amount)
	{
		ID = _id;
		item = _item;
		amount = _amount;
	}
	
	public void AddAmount(int value)
	{
		amount += value;
	}
}