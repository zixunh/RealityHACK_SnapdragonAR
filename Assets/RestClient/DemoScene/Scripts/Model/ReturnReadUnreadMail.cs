using System;
using Models;
using System.Collections.Generic;


namespace Models
{
	[Serializable]
	public class ReturnReadUnreadMail
	{
		public int status;
		public int unread_count;
		public List<SingleMail> unread_preview;
		public SingleMail latest_preview;
		public override string ToString()
		{
			return UnityEngine.JsonUtility.ToJson(this, true);
		}
	}
}
