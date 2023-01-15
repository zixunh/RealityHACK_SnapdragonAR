using System;

namespace Models
{
	[Serializable]
	public class ReadUnreadMail
	{
		public string readmode;
		public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}
	}
}

