using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Manager_SceneSpawner : MonoBehaviour
{
    public List<SceneLoadout> sceneLoadouts;

    private SceneLoadout currentLoadout = null;
    private GameObject cam;
    private GameObject player;

    //****************************************************************************************************
    private void Awake()
    {
        SceneManager.sceneLoaded += delegate { SpawnSceneObjects(); };
    }

    //****************************************************************************************************
    public void SpawnSceneObjects()
    {
        GetCurrentLoadout();

        if (currentLoadout)
        {
            foreach (SceneObject item in currentLoadout.spawnObjects)
            {
                if (item.prefab.name == "Camera")
                {
                    cam = Instantiate(item.prefab, item.spawnPosition, item.spawnRotation);
                }
                else if (item.prefab.name == "Player")
                {
                    player = Instantiate(item.prefab, item.spawnPosition, item.spawnRotation);
                }
                else
                {
                    Instantiate(item.prefab, item.spawnPosition, item.spawnRotation);
                }

                if (cam != null && player != null)
                {
                    cam.GetComponentInChildren<CinemachineVirtualCamera>().Follow = player.transform;
                }
            }
        }
    }

    //****************************************************************************************************
    private void GetCurrentLoadout()
    {
        currentLoadout = sceneLoadouts.Find(x => x.sceneName == SceneManager.GetActiveScene().name);
    }
}
