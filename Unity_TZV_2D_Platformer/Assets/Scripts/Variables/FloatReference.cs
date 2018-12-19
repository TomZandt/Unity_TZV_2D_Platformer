using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FloatReference
{
    public bool useLocal = true;
    public float localVariable;
    public FloatVariable sharedVariable;

    //****************************************************************************************************
    public float value
    {
        // condition ? consequence : alternative
        get { return useLocal ? localVariable : sharedVariable.value; }
    }
}
