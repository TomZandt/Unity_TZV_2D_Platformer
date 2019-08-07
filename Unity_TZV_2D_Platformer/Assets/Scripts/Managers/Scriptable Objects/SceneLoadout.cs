using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneLoadout", menuName = "TZV/ScriptableObjects/SceneLoadout", order = 1)]
public class SceneLoadout : ScriptableObject 
{
    public string sceneName;
    public List<SceneObject> spawnObjects;
}
