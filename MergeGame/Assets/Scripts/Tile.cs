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

    /// <summary>
    /// This method sets the entity for the tile. If there is already a current entity,
    /// it combines the two entities, plays particle effect and sound, and creates a new 
    /// entity with a higher level. Otherwise, it just sets the current entity, plays particle
    /// effect and sound, and sets the entity level. Finally, it updates the sprite of the 
    /// tile based on whether it's full or empty
    /// </summary>
    /// <param name="entity"></param>
    public void SetEntity(Entity entity)
    {
        if (entity)
        {
            if (currentEntity)
            {
                currentEntity.gameObject.SetActive(false);
                entity.gameObject.SetActive(false);

                Clear();

                Entity newEntity = Instantiate(entityList[entityLevel + 1], transform.position, Quaternion.identity);

                currentEntity = newEntity;
                currentEntity.SetParent(transform);
                entityLevel = currentEntity.GetEntityLevel();
            }
            else
            {
                currentEntity = entity;
                currentEntity.SetParent(transform);
                entityLevel = currentEntity.GetEntityLevel();
            }

            spriteRenderer.sprite = GetIsFull() ? fullTileSprite : emptyTileSprite;
        }
    }

    /// <summary>
    /// This method returns the current entity attached to the tile
    /// </summary>
    /// <returns></returns>
    public Entity GetEntity()
    {
        return currentEntity;
    }

    /// <summary>
    /// This function clears the current entity on the tile, making it empty, and sets the tile
    /// sprite to the emptyTileSprite
    /// </summary>
    public void Clear()
    {
        currentEntity = null;
        spriteRenderer.sprite = emptyTileSprite;
    }

    /// <summary>
    /// This function returns a boolean value indicating whether the tile is currently holding
    /// an entity or not. If there is an entity present, it returns true, otherwise it returns false
    /// </summary>
    /// <returns></returns>
    public bool GetIsFull()
    {
        return currentEntity;
    }
}
