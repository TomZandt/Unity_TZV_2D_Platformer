using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneObject : ScriptableObject
{
    public GameObject prefab;
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;
}
