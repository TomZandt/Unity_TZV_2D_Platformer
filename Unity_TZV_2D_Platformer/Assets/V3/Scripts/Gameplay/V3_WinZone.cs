using UnityEngine;

public class V3_WinZone : MonoBehaviour
{
    public ScriptableObjectArchitecture.GameEvent gameEvent_playerInWinZone;

    private int playerLayer;        // The layer the player game object is on

    //****************************************************************************************************
    private void Start()
    {
        // Get the integer representation of the "Player" layer
        playerLayer = LayerMask.NameToLayer("Player");
    }

    //****************************************************************************************************
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the collision wasn't with the player, exit
        if (collision.gameObject.layer != playerLayer)
        {
            return;
        }

        // Raise a game event to say the player has collided with the win zone
        Debug.Log("gameEvent_playerInWinZone Raised - V3_WinZone");
        gameEvent_playerInWinZone.Raise();
    }
}
