using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Connection from input field to stones
/// Informs stones about changes and duration of their hit time
/// </summary>
public class TimeFieldStorage : MonoBehaviour
{
    ValueClamp infoSource = null;

    void Start()
    {
        infoSource = GetComponent<ValueClamp>();
        if (infoSource)
            infoSource.onValueChange += ChangeStoneHitDuration;
        ChangeStoneHitDuration(2);
    }


    public void ChangeStoneHitDuration(int _duration)
    {
        Stone.SetHitDuration(_duration);
    }
}
