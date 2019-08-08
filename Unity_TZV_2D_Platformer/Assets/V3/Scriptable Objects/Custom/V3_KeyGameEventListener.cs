using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "V3_Key_GameEventListener")]
	public sealed class V3_KeyGameEventListener : BaseGameEventListener<V3_Key, V3_KeyGameEvent, V3_KeyUnityEvent>
	{
	}
}
