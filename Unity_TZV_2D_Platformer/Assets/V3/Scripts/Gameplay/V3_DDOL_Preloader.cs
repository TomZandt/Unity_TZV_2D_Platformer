using UnityEngine;
using UnityEngine.SceneManagement;

public class V3_DDOL_Preloader : MonoBehaviour
{
    //****************************************************************************************************
    void Awake()
    {
        GameObject check = GameObject.Find("__DontDestroy");

        if (check == null)
            SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
