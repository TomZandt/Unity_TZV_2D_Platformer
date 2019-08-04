///****************************************************************************************************
// Reference to https://www.youtube.com/watch?v=4lgT3CnGLMg&list=PLX2vGYjWbI0REfhDHPpdIBjjrzDHDP-xT&index=15
// This script is responsible for detecting collision with the player and letting the Game Manager know
//****************************************************************************************************

using UnityEngine;
using UnityEngine.SceneManagement;

public class V3_WinZone : MonoBehaviour
{
    private int nextScene = -1;    // The next scene
    private int playerLayer;    // The layer the player game object is on

    //****************************************************************************************************
    private void Start()
    {
        // find next scene
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if(nextScene > SceneManager.sceneCountInBuildSettings - 1)
        {
            nextScene = -1;
        }

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
        //V3_GameManager.PlayerWon();

        if(nextScene == -1)
        {
            // Tell the Game Manager that the player won
            V3_GameManager.PlayerWon();
        }
        else
        {
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }
    }
}
