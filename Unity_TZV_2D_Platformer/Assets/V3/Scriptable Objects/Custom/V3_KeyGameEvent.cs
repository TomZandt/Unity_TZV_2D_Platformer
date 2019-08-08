using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "V3_KeyGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "V3_Key",
	    order = 120)]
	public sealed class V3_KeyGameEvent : GameEventBase<V3_Key>
	{
	}
}