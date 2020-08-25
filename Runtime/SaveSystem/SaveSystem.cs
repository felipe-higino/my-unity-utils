using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "SaveSystem", menuName = "Felipe Utils/Save/SaveSystem", order =1)]
public class SaveSystem : ScriptableObject
{
	public void showData()
	{
		var json = JsonUtility.ToJson(SO_DB.Database);
		Debug.Log(json);
	}
	[ContextMenuItem("Show data as json", "showData")]
	[SerializeField] 
	SO_Database SO_DB;

	private string archieve = "/Save.bin";

	public void SaveDatabase()
	{
		var SavePath = Application.persistentDataPath + archieve;
		FileStream stream = new FileStream(SavePath, FileMode.Create);

		//binary save
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(stream, SO_DB.Database);

		stream.Close();
	}

	public void LoadDatabase()
	{
		var SavePath = Application.persistentDataPath + archieve;
		if (File.Exists(SavePath))
		{
			FileStream stream = new FileStream(SavePath, FileMode.Open);
			BinaryFormatter formatter = new BinaryFormatter();

			var loadedData = formatter.Deserialize(stream) as TDatabaseMain;

			if (loadedData != null)
				SO_DB.Database = loadedData;

			stream.Close();
		}
	}
}