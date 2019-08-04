using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class V3_ActiveWhenNot : MonoBehaviour
{
    public int sceneNumber = 0;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex > sceneNumber)
            gameObject.SetActive(true);
    }
}
