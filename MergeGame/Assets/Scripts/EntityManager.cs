using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoSingleton<EntityManager>
{
    [Header("Entities")]
    [SerializeField]
    private List<Entity> entityList;

    public Entity GetEntityWithIndex(int index)
    {
        return entityList[index];
    }

    public int GetEntityListCount()
    {
        return entityList.Count;
    }
}
