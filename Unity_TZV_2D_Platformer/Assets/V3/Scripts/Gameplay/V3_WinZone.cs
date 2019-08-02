///****************************************************************************************************
// Reference to https://www.youtube.com/watch?v=4lgT3CnGLMg&list=PLX2vGYjWbI0REfhDHPpdIBjjrzDHDP-xT&index=15
// This script is responsible for detecting collision with the player and letting the Game Manager know
//****************************************************************************************************

using UnityEngine;

public class V3_WinZone : MonoBehaviour
{
    private int playerLayer;    //The layer the player game object is on

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

        // Tell the Game Manager that the player won
        V3_GameManager.PlayerWon();
    }
}
