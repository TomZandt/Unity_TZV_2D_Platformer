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
        Debug.Log("LoadNextScene() Called - V3_SceneManager");

        nextSceneID = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneID > SceneManager.sceneCountInBuildSettings - 1)
            nextSceneID = -1;

        if (nextSceneID <= -1)
        {
            Debug.Log("gameEvent_playerCompletedGame Raised - V3_SceneManager");
            gameEvent_playerCompletedGame.Raise();
        }
        else
        {
            LoadSceneIDWithSave(nextSceneID);
        }
    }

    //****************************************************************************************************
    public void ReloadActiveScene()
    {
        Debug.Log("ReloadActiveScene() Called - V3_SceneManager");
        LoadSceneID(SceneManager.GetActiveScene().buildIndex);
    }

    //****************************************************************************************************
    public void LoadSceneID(int _sceneId)
    {
        Debug.Log("LoadSceneID(int _sceneId) Called - V3_SceneManager");

        Debug.Log("intGameEvent_loadNextScene Raised - V3_SceneManager (Scene: " + _sceneId + ")");
        intGameEvent_loadNextScene.Raise(_sceneId);

        SceneManager.LoadScene(_sceneId, LoadSceneMode.Single);
    }

    //****************************************************************************************************
    private void LoadSceneIDWithSave(int _sceneId)
    {
        Debug.Log("LoadSceneIDWithSave(int _sceneId) Called - V3_SceneManager");

        Debug.Log("PlayerPrefs.SetInt Raised - V3_SceneManager (Scene: " + _sceneId + ")");
        if (_sceneId > PlayerPrefs.GetInt("levelReachedByPlayer"))
            PlayerPrefs.SetInt("levelReachedByPlayer", _sceneId);

        Debug.Log("intGameEvent_loadNextScene Raised - V3_SceneManager (Scene: " + _sceneId + ")");
        intGameEvent_loadNextScene.Raise(_sceneId);

        SceneManager.LoadScene(_sceneId, LoadSceneMode.Single);
    }
}
