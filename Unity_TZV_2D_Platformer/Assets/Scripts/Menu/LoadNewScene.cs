using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    public void Load(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}
