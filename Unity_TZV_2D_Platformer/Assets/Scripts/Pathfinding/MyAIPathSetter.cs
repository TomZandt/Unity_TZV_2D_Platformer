using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath))]

public class MyAIPathSetter : MonoBehaviour
{
    private AIPath aiPath;

    //****************************************************************************************************
    private void Start()
    {
        aiPath = GetComponent<AIPath>();
    }

    //****************************************************************************************************
    public void setCanSearch(bool _value)
    {
        aiPath.canSearch = _value;
    }

    //****************************************************************************************************
    public bool getCanSearch()
    {
        return aiPath.canSearch;
    }
}
