// This script controls the orb collectables. It is responsible for detecting collision
// with the player and reporting it to the game manager. Additionally, since the orb
// is a part of the level it will need to register itself with the game manager

using UnityEngine;

public class V3_Orb : MonoBehaviour
{
    public GameObject explosionVFXPrefab;   // The visual effects for orb collection

    private int playerLayerint;				// The layer the player game object is on

    //****************************************************************************************************
    private void Start()
    {
        // Get the integer representation of the "Player" layer
        playerLayerint = LayerMask.NameToLayer("Player");

        // Register this orb with the game manager
        V3_GameManager.RegisterOrb(this);
    }

    //****************************************************************************************************
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the collided object isn't on the Player layer, exit. This is more efficient than string comparisons using Tags
        if (collision.gameObject.layer != playerLayerint)
        {
            return;
        }

        // The orb has been touched by the Player, so instantiate an explosion prefab at this location and rotation
        if (explosionVFXPrefab != null)
        {
            Instantiate(explosionVFXPrefab, transform.position, transform.rotation);
        }

        // Tell audio manager to play orb collection audio
        V3_AudioManager.PlayOrbCollectionAudio();

        // Tell the game manager that this orb was collected
        V3_GameManager.PlayerGrabbedOrb(this);

        // Deactivate this orb to hide it and prevent further collection
        gameObject.SetActive(false);
    }
}
