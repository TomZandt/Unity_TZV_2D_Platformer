using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V3_UIManager2 : MonoBehaviour
{
    public ScriptableObjectArchitecture.IntGameEvent intGameEvent_PlayerDeathCountUpdated;
    public ScriptableObjectArchitecture.IntVariable intVariable_deathCount;
    public ScriptableObjectArchitecture.StringGameEvent StringGameEvent_playerPlayTimeUpdated;
    public ScriptableObjectArchitecture.StringVariable stringVariable_playerPlayTime;

    private float totalPlayTime;

    //****************************************************************************************************
    private void Start()
    {
        Debug.Log("UpdateDeathCount Raised - V3_UIManager2");
        intGameEvent_PlayerDeathCountUpdated.Raise(intVariable_deathCount.Value);

        InvokeRepeating("Invoker", 0f, 1f);
    }

    //****************************************************************************************************
    private void Update()
    {
        stringVariable_playerPlayTime.Value = FormatPlayTime(Time.unscaledTime);
    }

    //****************************************************************************************************
    public void UpdateDeathCount()
    {
        intVariable_deathCount.Value++;

        Debug.Log("UpdateDeathCount Raised - V3_UIManager2");
        intGameEvent_PlayerDeathCountUpdated.Raise(intVariable_deathCount.Value);
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

    //****************************************************************************************************
    private void Invoker()
    {
        StringGameEvent_playerPlayTimeUpdated.Raise(stringVariable_playerPlayTime.Value);
    }
}
