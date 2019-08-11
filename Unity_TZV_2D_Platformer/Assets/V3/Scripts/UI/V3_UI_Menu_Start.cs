//****************************************************************************************************
// Reference: https://www.youtube.com/watch?v=AQpDtrNJAEU&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0&index=27
//****************************************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class V3_UI_Menu_Start : MonoBehaviour
{
    public ScriptableObjectArchitecture.IntGameEvent intGameEvent_PlayerRequestedScene;
    public Button[] levelButtons;

    //****************************************************************************************************
    private void Start()
    {
        int levelReachedByPlayer = PlayerPrefs.GetInt("levelReachedByPlayer", 2);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 2 > levelReachedByPlayer)
                levelButtons[i].interactable = false;
        }
    }

    //****************************************************************************************************
    public void RequestNextScene(int _sceneIndex)
    {
        Debug.Log("RequestNextScene(int _sceneIndex) Called - V3_UI_Menu_Start");

        Debug.Log("intGameEvent_PlayerRequestedScene Raised - V3_UI_Menu_Start (Scene: " + _sceneIndex + ")");
        intGameEvent_PlayerRequestedScene.Raise(_sceneIndex);
    }

    //****************************************************************************************************
    public void QuitGame()
    {
        Debug.LogWarning("QuitGame called - TZV V3_UI_Menu_Start");
        Application.Quit();
    }
}
