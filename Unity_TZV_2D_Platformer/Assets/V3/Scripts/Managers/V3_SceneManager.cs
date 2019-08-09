using UnityEngine;
using UnityEngine.SceneManagement;

public class V3_SceneManager : MonoBehaviour
{
    public ScriptableObjectArchitecture.IntVariable intVariable_CurrentScene;
    public ScriptableObjectArchitecture.GameEvent gameEvent_playerCompletedGame;
    public ScriptableObjectArchitecture.IntGameEvent intGameEvent_loadNextScene;

    private int nextSceneID;

    //****************************************************************************************************
    private void Update()
    {
        if (intVariable_CurrentScene.Value != SceneManager.GetActiveScene().buildIndex)
            intVariable_CurrentScene.Value = SceneManager.GetActiveScene().buildIndex;
    }

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
            LoadSceneID(nextSceneID);
        }
    }

    //****************************************************************************************************
    public void LoadSceneID(int _sceneId)
    {
        Debug.Log("LoadNextScene Raised - V3_SceneManager (Scene: " + _sceneId + ")");
        intGameEvent_loadNextScene.Raise(_sceneId);

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
