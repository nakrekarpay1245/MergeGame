using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    [SerializeField]
    private float uiDelay = 0.125f;

    public float GetUIDelay()
    {
        return uiDelay;
    }

    public float GetUIDelay2()
    {
        return uiDelay * 2;
    }

    public float GetUIDelay4()
    {
        return uiDelay * 4;
    }

    public float GetUIDelay8()
    {
        return uiDelay * 8;
    }
}
