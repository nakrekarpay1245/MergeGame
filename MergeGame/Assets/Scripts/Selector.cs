using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Selector : MonoSingleton<Selector>
{
    [Header("Tile")]
    [HideInInspector]
    public List<Tile> activeTileList;

    private Tile firstTile = null;
    private Tile lastTile = null;

    [Header("Entity")]
    [SerializeField]
    private Entity primitiveEntityPrefab = null;
    private Entity currentEntity = null;

    private int randomCall;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float nearestDistance = float.MaxValue;

            for (int i = 0; i < activeTileList.Count; i++)
            {
                Tile tile = activeTileList[i];
                float distance = Vector2.Distance(tile.transform.position,
                    Camera.main.ScreenToWorldPoint(Input.mousePosition));

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    firstTile = tile;
                }
            }

            currentEntity = firstTile.GetEntity();
        }

        if (Input.GetMouseButton(0) && currentEntity)
        {
            if (currentEntity)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 entityPosition = new Vector3(mousePosition.x, mousePosition.y, 0);
                currentEntity.transform.position = entityPosition;

                float nearestDistance = float.MaxValue;

                for (int i = 0; i < activeTileList.Count; i++)
                {
                    Tile tile = activeTileList[i];
                    float distance = Vector2.Distance(tile.transform.position,
                        Camera.main.ScreenToWorldPoint(Input.mousePosition));

                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;                        
                        lastTile = tile;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && currentEntity)
        {
            float nearestDistance = float.MaxValue;

            for (int i = 0; i < activeTileList.Count; i++)
            {
                Tile tile = activeTileList[i];
                float distance = Vector2.Distance(tile.transform.position,
                    Camera.main.ScreenToWorldPoint(Input.mousePosition));

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    lastTile = tile;
                }
            }

            if (!lastTile.GetIsFull())
            {
                firstTile.Clear();
                lastTile.SetEntity(currentEntity);
                currentEntity = null;
            }
            else
            {
                if (lastTile.GetEntity().GetEntityLevel() == currentEntity.GetEntityLevel())
                {
                    firstTile.Clear();
                    lastTile.SetEntity(currentEntity);
                    currentEntity = null;
                }
                else
                {
                    currentEntity.ResetPosition();
                }
            }
        }
    }

    public void ProduceEntity()
    {
        randomCall = 0;
        Tile randomTile = GetRandomTile();
        Entity generatedEntity = randomTile ?
            Instantiate(primitiveEntityPrefab, randomTile.transform) : null;
        randomTile?.SetEntity(generatedEntity);
    }

    public void AddTile(Tile tile)
    {
        if (!activeTileList.Contains(tile))
        {
            activeTileList.Add(tile);
        }
    }

    private Tile GetRandomTile()
    {
        if (randomCall < activeTileList.Count)
        {
            Tile randomTile = activeTileList[Random.Range(0, activeTileList.Count)];
            if (randomTile.GetIsFull())
            {
                randomCall++;
                return GetRandomTile();
            }
            randomCall++;
            return randomTile;
        }
        randomCall++;
        return null;
    }
}