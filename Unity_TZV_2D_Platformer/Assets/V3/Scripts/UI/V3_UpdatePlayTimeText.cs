using TMPro;
using UnityEngine;

public class V3_UpdatePlayTimeText : MonoBehaviour
{
    public TextMeshProUGUI textComp;

    public ScriptableObjectArchitecture.StringVariable stringVariable_PlayerPlayTime;

    //****************************************************************************************************
    private void Update()
    {
        if (textComp.ToString() != stringVariable_PlayerPlayTime.Value.ToString())
            textComp.SetText(stringVariable_PlayerPlayTime.Value.ToString());
    }
}
