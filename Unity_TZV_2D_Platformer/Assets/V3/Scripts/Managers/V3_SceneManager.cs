using UnityEngine;
using UnityEngine.SceneManagement;

public class V3_SceneManager : MonoBehaviour
{
    public ScriptableObjectArchitecture.GameEvent gameEvent_playerCompletedGame;
    public ScriptableObjectArchitecture.IntGameEvent intGameEvent_loadNextScene;

    private int nextSceneID;

    //****************************************************************************************************
    public void LoadNextScene()
    {
        nextSceneID = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneID > SceneManager.sceneCountInBuildSettings - 1)
            nextSceneID = -1;

        if (nextSceneID <= -1)
        {
            Debug.Log("playerCompletedGame Raised - V3_SceneManager");
            gameEvent_playerCompletedGame.Raise();
        }
        else
        {
            Debug.Log("LoadNextScene Raised - V3_SceneManager");
            intGameEvent_loadNextScene.Raise(nextSceneID);
            SceneManager.LoadScene(nextSceneID, LoadSceneMode.Single);
        }
    }

    //****************************************************************************************************
    public void LoadSceneID(int _sceneId)
    {
        Debug.Log("LoadSceneID Called - V3_SceneManager");
        SceneManager.LoadScene(_sceneId, LoadSceneMode.Single);
    }

    //****************************************************************************************************
    public void ReloadActiveScene()
    {
        Debug.Log("ReloadActiveScene Called - V3_SceneManager");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    //****************************************************************************************************
    public void RestartGame()
    {
        Debug.Log("RestartGame Called - V3_SceneManager");
        LoadSceneID(0);
    }
}
