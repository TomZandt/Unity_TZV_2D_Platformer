///****************************************************************************************************
// Reference to https://www.youtube.com/watch?v=4lgT3CnGLMg&list=PLX2vGYjWbI0REfhDHPpdIBjjrzDHDP-xT&index=15
// This script handles detecting collisions with traps and telling the Game Manager when the player dies
//****************************************************************************************************

using UnityEngine;

public class V3_PlayerHealth : MonoBehaviour
{
    public GameObject deathVFXPrefab;   // The visual effects for player death

    private int trapsLayer;			    // The layer the traps are on
    private bool isAlive = true;        // Stores the player's "alive" state

    //****************************************************************************************************
    private void Start()
    {
        //Get the integer representation of the "Traps" layer
        trapsLayer = LayerMask.NameToLayer("Trap");
    }

    //****************************************************************************************************
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the collided object isn't on the Traps layer OR if the player isn't currently alive, exit. This is more efficient than string comparisons using Tags
        if (collision.gameObject.layer != trapsLayer || !isAlive)
        {
            return;
        }

        // Trap was hit, so set the player's alive state to false
        isAlive = false;

        // Instantiate the death particle effects prefab at player's location
        if (deathVFXPrefab != null)
        {
            Instantiate(deathVFXPrefab, transform.position, transform.rotation);
        }

        // Disable player game object
        gameObject.SetActive(false);

        // Tell the Game Manager that the player died and tell the Audio Manager to play the death audio
        V3_GameManager.PlayerDied();
        V3_AudioManager.PlayDeathAudio();
    }
}
