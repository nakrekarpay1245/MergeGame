using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    private int entityLevel;

    public int GetEntityLevel()
    {
        return entityLevel;
    }

    public void ResetPosition()
    {
        transform.localPosition = Vector3.up * 0.5f;
    }

    public void SetParent(Transform _transform)
    {
        transform.parent = _transform;
        ResetPosition();
    }
}
