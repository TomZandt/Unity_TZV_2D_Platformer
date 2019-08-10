using UnityEngine;
using Rewired.UI.ControlMapper;

public class V3_PauseManager : MonoBehaviour
{
    public V3_SO_Input inputSO;
    public GameObject PauseUI;

    public ScriptableObjectArchitecture.BoolVariable boolVariable_gameIsPaused;

    //****************************************************************************************************
    private void Start()
    {
        PauseUI.SetActive(false);
    }

    //****************************************************************************************************
    private void Update()
    {
        CheckForPause();
    }

    //****************************************************************************************************
    private void CheckForPause()
    {
        // Toggle game is paused
        if (inputSO.isPausePressed)
        {
            if (boolVariable_gameIsPaused.Value)
                Resume();
            else
                Pause();
        }
    }

    //****************************************************************************************************
    public void Pause()
    {
        // Pause the game time
        Time.timeScale = 0f;

        // Open the menu
        PauseUI.SetActive(true);

        // Update game is paused bool
        boolVariable_gameIsPaused.Value = true;
    }

    //****************************************************************************************************
    public void Resume()
    {
        // Update game is paused bool
        boolVariable_gameIsPaused.Value = false;

        // Reset Input to clear unwanted
        inputSO.Reset();

        // Close the menu
        PauseUI.SetActive(false);

        // UnPause the game time
        Time.timeScale = 1f;
    }

    //****************************************************************************************************
    public void QuitGame()
    {
        Debug.LogWarning("QuitGame called - TZV V3_UI_Menu_Start");
        Application.Quit();
    }
}
