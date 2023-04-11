using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Entity List")]
    [SerializeField]
    private List<Entity> entityList;

    private Entity currentEntity;

    [Header("Tile Visualize")]
    [SerializeField]
    private Sprite emptyTileSprite;
    [SerializeField]
    private Sprite fullTileSprite;

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
            if (currentEntity)
            {
                currentEntity.gameObject.SetActive(false);
                entity.gameObject.SetActive(false);

                Clear();

                Entity newEntity = Instantiate(entityList[entityLevel + 1]);
                SetEntity(newEntity);
            }
            else
            {
                currentEntity = entity;
                currentEntity.SetParent(transform);
                entityLevel = currentEntity.GetEntityLevel();
            }

            spriteRenderer.sprite = GetIsFull() ? fullTileSprite : emptyTileSprite;
        }
        else
        {
            Debug.LogWarning("The entity is null!");
        }
    }

    public Entity GetEntity()
    {
        return currentEntity;
    }

    public void Clear()
    {
        currentEntity = null;
        spriteRenderer.sprite = emptyTileSprite;
    }

    public bool GetIsFull()
    {
        return currentEntity;
    }
}
