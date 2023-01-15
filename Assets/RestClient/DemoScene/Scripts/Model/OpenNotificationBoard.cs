using System;

namespace Models
{
	[Serializable]
	public class OpenNotificationBoard
	{
		public Boolean deep;
		public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}
	}
}

