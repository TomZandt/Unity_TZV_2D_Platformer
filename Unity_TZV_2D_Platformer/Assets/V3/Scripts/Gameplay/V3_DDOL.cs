using UnityEngine;
using UnityEngine.SceneManagement;

public class V3_DDOL : MonoBehaviour
{
    //****************************************************************************************************
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    //****************************************************************************************************
    private void Start()
    {
        SceneManager.LoadScene(1);
    }
}
