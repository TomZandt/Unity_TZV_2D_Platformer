using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V3_GameManager2 : MonoBehaviour
{
    [Header("Events")]
    public ScriptableObjectArchitecture.ObjectCollection KeyCollection;
    public ScriptableObjectArchitecture.GameEvent GameEvent_PlayerHasAllKeys;

    //****************************************************************************************************
    private void Start()
    {
        ClearCollection();
    }

    //****************************************************************************************************
    public void RegisterKey(V3_Key _key)
    {
        Debug.Log("RegisterKey called - V3_GameManager2" + _key.name);

        // If the list of keys doesn't already contain this one, then...
        if (!KeyCollection.Contains(_key))
        {
            // Then lets add it
            KeyCollection.Add(_key);
        }
    }

    //****************************************************************************************************
    public void PlayerGrabbedKey(V3_Key _key)
    {
        Debug.Log("PlayerGrabbedKey called - V3_GameManager2" + _key.name);

        // If the key collection doesn't have this key...
        if (!KeyCollection.Contains(_key))
        {
            // Exit
            return;
        }

        DeregisterKey(_key);

        CheckIfPlayerHasAllKeys();
    }

    //****************************************************************************************************
    public void DeregisterKey(V3_Key _key)
    {
        Debug.Log("DeregisterKey called - V3_GameManager2" + _key.name);

        // If the key is in the list
        if (KeyCollection.Contains(_key))
            // Remove the key
            KeyCollection.Remove(_key);
    }

    //****************************************************************************************************
    public void CheckIfPlayerHasAllKeys()
    {
        Debug.Log("CheckIfPlayerHasAllKeys called - V3_GameManager2");

        // If there are no more keys...
        if (KeyCollection.Count == 0)
        {
            // Tell the door to open
            Debug.Log("GameEvent_PlayerHasAllKeys Raised - V3_GameManager2");
            GameEvent_PlayerHasAllKeys.Raise();
        }
    }

    //****************************************************************************************************
    public void ClearCollection()
    {
        Debug.Log("KeyCollection.Clear called - V3_GameManager2");
        KeyCollection.Clear();
    }
}
