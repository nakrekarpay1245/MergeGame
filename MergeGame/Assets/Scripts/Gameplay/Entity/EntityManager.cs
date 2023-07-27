using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoSingleton<EntityManager>
{
    [Header("Entities")]
    [SerializeField]
    private List<Entity> _entityList;

    public Entity GetEntityWithIndex(int index)
    {
        return _entityList[index];
    }
}