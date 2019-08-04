using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V3_DontDestroyMe : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
