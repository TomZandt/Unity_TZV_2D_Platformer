using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class V3_KeyReference : BaseReference<V3_Key, V3_KeyVariable>
	{
	    public V3_KeyReference() : base() { }
	    public V3_KeyReference(V3_Key value) : base(value) { }
	}
}