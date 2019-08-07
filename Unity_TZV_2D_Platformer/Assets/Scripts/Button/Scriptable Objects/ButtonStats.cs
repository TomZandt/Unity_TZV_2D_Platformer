using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonStats", menuName = "TZV/ScriptableObjects/ButtonStats", order = 1)]
public class ButtonStats : ScriptableObject
{
    [Header("Health")]
    public float buttonMaxHealth = 1f;
    public float buttonHealth = 1f;
}
