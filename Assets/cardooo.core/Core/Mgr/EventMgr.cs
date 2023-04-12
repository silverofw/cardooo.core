using UnityEngine;
using System.Collections;

namespace cardooo.core
{
	public class EventMgr : Singleton<EventMgr>
	{
		public EventHandler MainHandler = new EventHandler();
	}
}

