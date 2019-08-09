using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V3_UIManager2 : MonoBehaviour
{
    public ScriptableObjectArchitecture.IntVariable intVariable_deathCount;
    public ScriptableObjectArchitecture.StringVariable stringVariable_playerPlayTime;
    public ScriptableObjectArchitecture.IntVariable intVariable_CurrentScene;

    public GameObject[] GameObjectsToShow;

    private float totalPlayTime;

    //****************************************************************************************************
    private void Update()
    {
        if (intVariable_CurrentScene.Value > 1)
        {
            for (int i = 0; i < GameObjectsToShow.Length; i++)
            {
                GameObjectsToShow[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < GameObjectsToShow.Length; i++)
            {
                GameObjectsToShow[i].SetActive(false);
            }

            if (intVariable_deathCount.Value != 0)
                intVariable_deathCount.Value = 0;
        }

        stringVariable_playerPlayTime.Value = FormatPlayTime(Time.unscaledTime);
    }

    //****************************************************************************************************
    public void UpdateDeathCount()
    {
        intVariable_deathCount.Value++;
    }

    //****************************************************************************************************
    public string FormatPlayTime(float _time)
    {
        //Take the time and convert it into the number of minutes and seconds
        int hours = (int)(_time / 3600);
        int minutes = (int)(_time / 60);
        float seconds = _time % 60f;

        //Create the string in the appropriate format for the time
        return hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
