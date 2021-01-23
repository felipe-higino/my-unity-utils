using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

namespace FelipeUtils.SaveSystem
{
	/// <summary>
	/// D is the database scriptable object inheritance
	/// T is the class model
	/// </summary>
	[Serializable]
	public abstract class SaveSystem<D,T> : ScriptableObject 
		where D : Database<T>
		where T : class
	{
		public void showData()
		{
			var json = JsonUtility.ToJson(SO_DB.dbClass);
			Debug.Log(json);
		}
		[ContextMenuItem("Show data as json", "showData")]
		[SerializeField] 
		D SO_DB = null;

		private string archieve = "/Save.bin";

		public void SaveDatabase()
		{
			var SavePath = Application.persistentDataPath + archieve;
			FileStream stream = new FileStream(SavePath, FileMode.Create);

			//binary save
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, SO_DB.dbClass);

			stream.Close();
		}

		public void LoadDatabase()
		{
			var SavePath = Application.persistentDataPath + archieve;
			if (File.Exists(SavePath))
			{
				FileStream stream = new FileStream(SavePath, FileMode.Open);
				BinaryFormatter formatter = new BinaryFormatter();

				var loadedData = formatter.Deserialize(stream) as T;

				if (loadedData != null)
					SO_DB.dbClass = loadedData;

				stream.Close();
			}
		}
	}
}
