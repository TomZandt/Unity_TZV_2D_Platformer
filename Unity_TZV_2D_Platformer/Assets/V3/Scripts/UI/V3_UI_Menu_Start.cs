using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class V3_UI_Menu_Start : MonoBehaviour
{
    //****************************************************************************************************
    public void LoadNextScene()
    {
        // If there is not another scene
        if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCountInBuildSettings - 1)
        {
            // Do nothing
            Debug.LogWarning("No scene to load - TZV V3_UI_Menu_Start");
            return;
        }

        // Load the next Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    //****************************************************************************************************
    public void QuitGame()
    {
        Debug.LogWarning("QuitGame called - TZV V3_UI_Menu_Start");
        Application.Quit();
    }
}
