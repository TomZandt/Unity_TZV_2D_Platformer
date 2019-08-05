using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "V3_SO_GameManager", menuName = "Scriptable Objects/V3_SO_GameManager", order = 1)]
public class V3_SO_GameManager : ScriptableObject
{
    public int levelIndex = 0;                      // The id of the level
    public bool isGameOver = false;                 // Is the game over
    public float totalGameTime = 0f;                // The total time of current game session
    public float deathSequenceDuration = 1.5f;      // How long player death takes before restarting

    public List<V3_Orb> orbs;                       // The list of orbs
    public V3_Door lockedDoor;                      // The scene door
    public V3_SceneFader sceneFader;                // The scene fader
    public int totalPlayerDeaths = 0;               // The total player deaths

    //****************************************************************************************************
    public void RegisterDoor(V3_Door _door)
    {
        // Record the door reference
        lockedDoor = _door;
    }
}
