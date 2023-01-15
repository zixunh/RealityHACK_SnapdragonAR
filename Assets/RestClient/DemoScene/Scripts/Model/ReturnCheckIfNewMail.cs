using System;

namespace Models
{
	[Serializable]
	public class ReturnCheckIfNewMail
	{
		public Boolean has_new;
        public int unread_count;
		public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}
	}
}

