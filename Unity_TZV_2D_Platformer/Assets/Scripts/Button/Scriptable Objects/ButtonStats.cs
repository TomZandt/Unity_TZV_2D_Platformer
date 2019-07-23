using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ButtonStats : ScriptableObject
{
    [Header("Health")]
    public float buttonMaxHealth = 1f;
    public float buttonHealth = 1f;
}
