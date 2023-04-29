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
}
