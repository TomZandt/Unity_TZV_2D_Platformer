using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneObject", menuName = "TZV/ScriptableObjects/SceneObject", order = 1)]
public class SceneObject : ScriptableObject
{
    public GameObject prefab;
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;
}
