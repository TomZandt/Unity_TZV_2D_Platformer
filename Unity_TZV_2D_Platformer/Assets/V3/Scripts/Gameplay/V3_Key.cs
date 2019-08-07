using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class V3_Key : MonoBehaviour
{
    [Header("Events")]
    public ScriptableObjectArchitecture.ObjectGameEvent ObjectGameEvent_KeyAddedToScene;
    public ScriptableObjectArchitecture.ObjectGameEvent ObjectGameEvent_PlayerPickedUpKey;

    private int playerLayerint; // The layer the player game object is on

    //****************************************************************************************************
    private void Start()
    {
        // Get the integer representation of the "Player" layer
        playerLayerint = LayerMask.NameToLayer("Player");

        // Raise a game event to say this is in the scene
        Debug.Log("ObjectGameEvent_KeyAddedToScene Raised - V3_Key");
        ObjectGameEvent_KeyAddedToScene.Raise(this.gameObject);
    }

    //****************************************************************************************************
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the collided object isn't on the Player layer, exit. This is more efficient than string comparisons using Tags
        if (collision.gameObject.layer != playerLayerint)
            return;

        // Tell the game manager that this key was picked up
        Debug.Log("ObjectGameEvent_PlayerPickedUpKey Raised - V3_Key");
        ObjectGameEvent_PlayerPickedUpKey.Raise(this.gameObject);
    }
}
