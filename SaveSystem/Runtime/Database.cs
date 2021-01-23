using UnityEngine;

namespace FelipeUtils.SaveSystem 
{
	/// <summary>
	/// Database scriptable object abstract.
	/// T is a serializable class to save.
	/// </summary>
	[System.Serializable]
	public abstract class Database<T> : ScriptableObject
		where T : class
	{
		public T dbClass = null;
	}

}