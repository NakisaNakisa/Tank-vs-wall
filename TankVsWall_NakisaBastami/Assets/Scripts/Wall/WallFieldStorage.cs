using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Connection from input field to wall manager
/// Informs wall manager about changes and number of stones
/// </summary>
public class WallFieldStorage : MonoBehaviour
{
    ValueClamp infoSource = null;
    WallManager wallManager = null;
    public int MaxStones => infoSource.GetMaxValue;

    void Start()
    {
        infoSource = GetComponent<ValueClamp>();
        if(infoSource)
            infoSource.onValueChange += OnValueChange;
    }

    public void SetWallManager(WallManager _wallManager)
    {
        wallManager = _wallManager;
    }

    public void OnValueChange(int _newValue)
    {
        if (wallManager)
            wallManager.SetStoneNumber(_newValue);
    }
}
