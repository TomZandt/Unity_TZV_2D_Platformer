using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class V3_UpdateUIText : MonoBehaviour
{
    public TextMeshProUGUI textComp;

    //****************************************************************************************************
    public void SetTextInt(int _value)
    {
        textComp.SetText(_value.ToString());
    }

    //****************************************************************************************************
    public void SetTextString(string _value)
    {
        textComp.SetText(_value.ToString());
    }
}
