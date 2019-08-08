using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "V3_KeyCollection.asset",
	    menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "V3_Key_Collection",
	    order = 120)]
	public class V3_KeyCollection : Collection<V3_Key>
	{
	}
}