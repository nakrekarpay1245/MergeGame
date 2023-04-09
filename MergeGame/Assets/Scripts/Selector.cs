using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public List<Tile> tileList;

    private Tile firstTile = null;
    private Tile lastTile = null;
    private Entity currentEntity = null;

    [SerializeField]
    private Entity primitiveEntityPrefab = null;

    private int randomCall;

    [SerializeField]
    private Tile tilePrefab;
    [SerializeField]
    private Transform tileParent;
    private void Awake()
    {
        GenerateTile();
    }

    private void GenerateTile()
    {
        float x = -3;
        float y = 3;
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                Tile newTile = Instantiate(tilePrefab, tileParent);
                newTile.transform.position = new Vector3(x, y, 0);
                tileList.Add(newTile);
                x++;
            }
            x = -3;
            y--;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float nearestDistance = float.MaxValue;

            for (int i = 0; i < tileList.Count; i++)
            {
                Tile tile = tileList[i];
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
            }
        }

        if (Input.GetMouseButtonUp(0) && currentEntity)
        {
            float nearestDistance = float.MaxValue;

            for (int i = 0; i < tileList.Count; i++)
            {
                Tile tile = tileList[i];
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
                currentEntity.transform.position = lastTile.transform.position;
                currentEntity.transform.parent = lastTile.transform;
                currentEntity = null;
            }
            else
            {
                if (lastTile.GetEntity().GetEntityLevel() == currentEntity.GetEntityLevel())
                {
                    firstTile.Clear();
                    lastTile.SetEntity(currentEntity);
                    //currentEntity.transform.position = lastTile.transform.position;
                    //currentEntity.transform.parent = lastTile.transform;
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
        Entity generatedEntity = randomTile ? Instantiate(primitiveEntityPrefab, randomTile.transform) : null;
        generatedEntity?.ResetPosition();
        randomTile?.SetEntity(generatedEntity);
    }

    private Tile GetRandomTile()
    {
        if (randomCall < tileList.Count)
        {
            Tile randomTile = tileList[Random.Range(0, tileList.Count)];
            if (randomTile.GetIsFull())
            {
                return GetRandomTile();
            }
            else
            {
                return randomTile;
            }
        }
        else
        {
            return null;
        }
        randomCall++;
    }
}