using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player_Health : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameObject[] icons;

    //****************************************************************************************************
    public void UpdateUIPlayerHealth()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < playerStats.playerHealth; i++)
        {
            icons[i].gameObject.SetActive(true);
        }
    }
}
