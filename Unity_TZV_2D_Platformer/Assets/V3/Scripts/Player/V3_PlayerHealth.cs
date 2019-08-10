using UnityEngine;

public class V3_PlayerHealth : MonoBehaviour
{
    public ScriptableObjectArchitecture.GameEvent gameEvent_PlayerDied;
    public V3_SO_Player playerSO;

    private int trapsLayer;			    // The layer the traps are on

    //****************************************************************************************************
    private void Start()
    {
        playerSO.isAlive = true;
        //Get the integer representation of the "Traps" layer
        trapsLayer = LayerMask.NameToLayer("Trap");
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

    private void ProcessDeath()
    {
        // Trap was hit, so set the player's alive state to false
        playerSO.isAlive = false;

        // Disable player game object
        gameObject.SetActive(false);

        Debug.Log("gameEvent_PlayerDied Called - V3_PlayerHealth");
        gameEvent_PlayerDied.Raise();
    }
}
