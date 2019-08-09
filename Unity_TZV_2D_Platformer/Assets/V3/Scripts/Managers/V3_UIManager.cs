using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V3_UIManager : MonoBehaviour
{
    public ScriptableObjectArchitecture.IntVariable intVariable_deathCount;
    public ScriptableObjectArchitecture.StringVariable stringVariable_playerPlayTime;
    public ScriptableObjectArchitecture.IntVariable intVariable_CurrentScene;

    public GameObject[] GameObjectsToShow;

    private float totalPlayTime = 0;
    private float menuTime = 0;
    private bool calcMenuTime = true;

    //****************************************************************************************************
    private void Update()
    {
        if (intVariable_CurrentScene.Value > 1)
        {
            if (calcMenuTime)
            {
                menuTime = Time.unscaledTime;
                calcMenuTime = false;
            }

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

        stringVariable_playerPlayTime.Value = FormatPlayTime(Time.unscaledTime - menuTime);
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

    ////****************************************************************************************************
    //private void CheckForPause()
    //{
    //    // Toggle game is paused
    //    if (player.GetButtonDown("Pause") && SceneManager.GetActiveScene().buildIndex != 0)
    //    {
    //        if (canPause)
    //        {
    //            Time.timeScale = 0;
    //            canPause = false;

    //            controlMapper.Open();
    //        }
    //        else
    //        {
    //            controlMapper.Close(true);

    //            playerInput.ClearInput();
    //            Time.timeScale = 1;
    //            canPause = true;
    //        }
    //    }
    //}
}
