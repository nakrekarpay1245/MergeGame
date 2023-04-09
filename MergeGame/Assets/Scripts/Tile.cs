using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public List<Entity> entityList;
    public Entity currentEntity;
    private bool isFull;

    [SerializeField]
    private Color emptyColor;
    [SerializeField]
    private Color fullColor;

    private SpriteRenderer spriteRenderer;

    private int entityLevel;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetEntity(GetComponentInChildren<Entity>());
    }

    public void SetEntity(Entity entity)
    {
        if (entity)
        {
            Debug.Log("SetEntity " + entity.name);
        }
        else
        {
            Debug.Log("Null Entity ");
        }
        isFull = entity;
        spriteRenderer.color = isFull ? fullColor : emptyColor;

        if (currentEntity)
        {
            entityLevel = currentEntity.GetEntityLevel();
            if (entity)
            {
                if (currentEntity.GetEntityLevel() == entity.GetEntityLevel())
                {
                    currentEntity.gameObject.SetActive(false);
                    entity.gameObject.SetActive(false);

                    Clear();
                    Entity newEntity = Instantiate(entityList[entityLevel + 1]);
                    SetEntity(newEntity);

                    Debug.Log("Level Up Tile: " + (entityLevel + 1));
                }
            }
        }
        else
        {
            Debug.Log("Else " + name);

            if (entity)
            {
                currentEntity = entity;
                currentEntity.SetParent(transform);
                currentEntity.ResetPosition();
            }
        }
    }

    public Entity GetEntity()
    {
        return currentEntity;
    }

    public void Clear()
    {
        isFull = false;
        currentEntity = null;
        spriteRenderer.color = emptyColor;
    }

    public bool GetIsFull()
    {
        return isFull;
    }
}
