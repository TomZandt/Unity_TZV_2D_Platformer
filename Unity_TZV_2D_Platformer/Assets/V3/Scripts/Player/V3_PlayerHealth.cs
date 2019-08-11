using UnityEngine;
using Rewired;

public class V3_PlayerHealth : MonoBehaviour
{
    public ScriptableObjectArchitecture.GameEvent gameEvent_PlayerDied;
    public V3_SO_Player playerSO;

    private Player player;              // Rewired player

    private int trapsLayer;			    // The layer the traps are on

    //****************************************************************************************************
    private void Start()
    {
        playerSO.isAlive = true;
        //Get the integer representation of the "Traps" layer
        trapsLayer = LayerMask.NameToLayer("Trap");

        // Assign the rewired player
        player = ReInput.players.GetPlayer(0); // 0 default for first player
    }

    //****************************************************************************************************
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the collided object isn't on the Traps layer OR if the player isn't currently alive, exit. This is more efficient than string comparisons using Tags
        if (collision.gameObject.layer != trapsLayer || !playerSO.isAlive)
        {
            return;
        }

        ProcessDeath();
    }

    //****************************************************************************************************
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the collided object isn't on the Traps layer OR if the player isn't currently alive, exit. This is more efficient than string comparisons using Tags
        if (collision.gameObject.layer != trapsLayer || !playerSO.isAlive)
        {
            return;
        }

        ProcessDeath();
    }

    //****************************************************************************************************
    private void ProcessDeath()
    {
        Vibrate();

        // Trap was hit, so set the player's alive state to false
        playerSO.isAlive = false;

        Debug.Log("gameEvent_PlayerDied Called - V3_PlayerHealth");
        gameEvent_PlayerDied.Raise();
    }

    //****************************************************************************************************
    public void Vibrate()
    {
        // Set vibration in all Joysticks assigned to the Player
        int motorIndex = 0; // the first motor
        float motorLevel = 1.0f; // full motor speed
        float duration = 0.25f; // 2 seconds

        player.SetVibration(motorIndex, motorLevel, duration);
    }
}
