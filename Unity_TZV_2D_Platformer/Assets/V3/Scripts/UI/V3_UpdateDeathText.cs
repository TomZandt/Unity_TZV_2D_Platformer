using TMPro;
using UnityEngine;

public class V3_UpdateDeathText : MonoBehaviour
{
    public TextMeshProUGUI textComp;

    public ScriptableObjectArchitecture.IntVariable intVariable_PlayerDeaths;

    //****************************************************************************************************
    private void Update()
    {
        if(textComp.ToString() != intVariable_PlayerDeaths.Value.ToString())
            textComp.SetText(intVariable_PlayerDeaths.Value.ToString());
    }
}
