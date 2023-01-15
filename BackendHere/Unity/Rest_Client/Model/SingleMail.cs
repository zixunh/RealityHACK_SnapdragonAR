using System;

namespace Models
{
	[Serializable]
	public class SingleMail
	{
		public string subtitle;
        public string message;
        public Boolean unread;
		public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}
	}
}