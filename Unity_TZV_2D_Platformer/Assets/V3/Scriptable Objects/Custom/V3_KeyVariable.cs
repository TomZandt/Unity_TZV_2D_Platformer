using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "V3_KeyVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "V3_Key",
	    order = 120)]
	public class V3_KeyVariable : BaseVariable<V3_Key>
	{
	}
}