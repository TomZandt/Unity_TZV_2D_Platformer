using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class SceneLoadout : ScriptableObject 
{
    public string sceneName;
    public List<SceneObject> spawnObjects;
}
