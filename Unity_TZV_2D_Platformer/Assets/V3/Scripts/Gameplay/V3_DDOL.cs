//****************************************************************************************************
// Reference: https://gamedev.stackexchange.com/questions/116009/in-unity-how-do-i-correctly-implement-the-singleton-pattern
//****************************************************************************************************

using UnityEngine;
using UnityEngine.SceneManagement;

public class V3_DDOL : MonoBehaviour
{
    private static V3_DDOL _instance;
    public static V3_DDOL Instance { get { return _instance; } }

    public bool loadFromMenu = true;

    //****************************************************************************************************
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(gameObject);
    }

    //****************************************************************************************************
    private void Start()
    {
        if (loadFromMenu)
            SceneManager.LoadScene(1);
    }
}
