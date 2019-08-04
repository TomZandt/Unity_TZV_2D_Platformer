// This script is a Manager that controls the UI HUD (deaths, time, and orbs) for the 
// project. All HUD UI commands are issued through the static methods of this class

using UnityEngine;
using TMPro;
using Rewired;
using Rewired.UI.ControlMapper;

public class V3_UIManager : MonoBehaviour
{
    //This class holds a static reference to itself to ensure that there will only be
    //one in existence. This is often referred to as a "singleton" design pattern. Other
    //scripts access this one through its public static methods
    private static V3_UIManager current;

    public TextMeshProUGUI orbText;         //Text element showing number of orbs
    public TextMeshProUGUI timeText;        //Text element showing amount of time
    public TextMeshProUGUI deathText;       //Text element showing number or deaths
    public TextMeshProUGUI gameOverText;    //Text element showing the Game Over message

    private ControlMapper controlMapper;
    private Player player;
    private V3_PlayerInput playerInput;
    private bool canPause = true;

    //****************************************************************************************************
    private void Awake()
    {
        //If an UIManager exists and it is not this...
        if (current != null && current != this)
        {
            //...destroy this and exit. There can be only one UIManager
            Destroy(gameObject);
            return;
        }

        //This is the current UIManager and it should persist between scene loads
        current = this;
        DontDestroyOnLoad(gameObject);

        // Assign the rewired player
        player = ReInput.players.GetPlayer(0); // 0 default for first player

        playerInput = FindObjectOfType<V3_PlayerInput>();
        controlMapper = FindObjectOfType<ControlMapper>();

        canPause = true;
    }

    //****************************************************************************************************
    private void Update()
    {
        CheckForPause();

        if (orbText == null)
        {
            orbText = GameObject.FindGameObjectWithTag("OrbUI").GetComponentInChildren<TextMeshProUGUI>();
        }

        if (timeText == null)
        {
            timeText = GameObject.FindGameObjectWithTag("TimeUI").GetComponentInChildren<TextMeshProUGUI>();
        }

        if (deathText == null)
        {
            deathText = GameObject.FindGameObjectWithTag("DeathsUI").GetComponentInChildren<TextMeshProUGUI>();
        }

        if (gameOverText == null)
        {
            gameOverText = GameObject.FindGameObjectWithTag("GameOverUI").GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    //****************************************************************************************************
    public static void UpdateOrbUI(int orbCount)
    {
        //If there is no current UIManager, exit
        if (current == null)
            return;

        //Update the text orb element
        current.orbText.text = orbCount.ToString();
    }

    //****************************************************************************************************
    public static void UpdateTimeUI(float time)
    {
        //If there is no current UIManager, exit
        if (current == null)
            return;

        //Take the time and convert it into the number of minutes and seconds
        int minutes = (int)(time / 60);
        float seconds = time % 60f;

        //Create the string in the appropriate format for the time
        current.timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    //****************************************************************************************************
    public static void UpdateDeathUI(int deathCount)
    {
        //If there is no current UIManager, exit
        if (current == null)
            return;

        //update the player death count element
        current.deathText.text = deathCount.ToString();
    }

    //****************************************************************************************************
    public static void DisplayGameOverText()
    {
        //If there is no current UIManager, exit
        if (current == null)
            return;

        //Show the game over text
        current.gameOverText.enabled = true;
        current.gameOverText.gameObject.SetActive(true);
    }

    //****************************************************************************************************
    private void CheckForPause()
    {
        // Toggle game is paused
        if (player.GetButtonDown("Pause"))
        {
            if (canPause)
            {
                Time.timeScale = 0;
                canPause = false;

                controlMapper.Open();
            }
            else
            {
                controlMapper.Close(true);

                playerInput.ClearInput();
                Time.timeScale = 1;
                canPause = true;
            }
        }
    }
}
