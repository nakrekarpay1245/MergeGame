using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoSingleton<Selector>
{
    private const float minimumSelectionDistance = 0.75f;

    [Header("Tile")]
    [HideInInspector]
    public List<Tile> activeTileList;

    private Tile firstTile = null;
    private Tile lastTile = null;

    [Header("Entity")]
    [SerializeField]
    private Entity primitiveEntityPrefab = null;
    private Entity currentEntity = null;

    private int randomTileCallCount;

    [Header("Auto Produce")]
    [SerializeField]
    private float autoProduceTime = 10;
    private float produceTimer;

    [Header("Auto Produce")]
    [SerializeField]
    private Image produceButtonFill;

    private Touch touch;

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        SelectFirstTileEntity();
    //    }

    //    if (Input.GetMouseButton(0) && currentEntity)
    //    {
    //        MoveCurrentEntity();
    //    }

    //    if (Input.GetMouseButtonUp(0) && currentEntity)
    //    {
    //        SelectLastTile();

    //        HandleEntityPlacement();

    //        firstTile = null;
    //        lastTile = null;
    //        currentEntity = null;
    //    }
    //}

    void Update()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                SelectFirstTileEntity();
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                MoveCurrentEntity();
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                SelectLastTile();
                if (lastTile && firstTile && currentEntity)
                {
                    HandleEntityPlacement();
                }
                firstTile = null;
                lastTile = null;
                currentEntity = null;
            }
        }
        else
        {
            currentEntity?.ResetPosition();
        }

        CalculateProduceTimer();
    }

    private void CalculateProduceTimer()
    {
        if (produceTimer >= autoProduceTime)
        {
            produceTimer = 0;
            ProduceEntity();
        }
        else
        {
            produceTimer += Time.deltaTime;
        }

        DisplayProduceTimer();
    }

    private void DisplayProduceTimer()
    {
        produceButtonFill.fillAmount = produceTimer / autoProduceTime;
    }

    /// <summary>
    /// Selects the first tile that the user clicks on and sets it as the starting 
    /// point of a move operation.If the user's click is too far from any tiles, no 
    /// tile is selected. If the selected tile is empty or has no entity, no entity 
    /// is selected for the move operation.
    /// </summary>
    private void SelectFirstTileEntity()
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        float nearestDistance = float.MaxValue;

        for (int i = 0; i < activeTileList.Count; i++)
        {
            Tile tile = activeTileList[i];
            float distance = Vector2.Distance(tile.transform.position, touchPosition);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                firstTile = tile;
            }
        }

        if (nearestDistance > minimumSelectionDistance)
        {
            firstTile = null;
            return;
        }

        currentEntity = firstTile.GetEntity();
        currentEntity?.Select();
    }

    /// <summary>
    /// This function moves the current Entity object to the position of the mouse cursor 
    /// on the screen. If there is no current Entity, this function does nothing.
    /// </summary>
    private void MoveCurrentEntity()
    {
        if (currentEntity)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 entityPosition = new Vector3(touchPosition.x, touchPosition.y, 0);
            currentEntity.transform.position = entityPosition;
        }
    }

    /// <summary>
    /// This function selects the Tile object closest to the position of the mouse cursor
    /// when the player releases the mouse button. It loops through all active Tile objects, 
    /// calculates the distance between each Tile and the mouse position, and selects the 
    /// Tile with the smallest distance. If the selected Tile is too far away or the same 
    /// as the first selected Tile, the function resets the current Entity position and 
    /// clears the lastTile variable
    /// </summary>
    private void SelectLastTile()
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        float nearestDistance = float.MaxValue;

        for (int i = 0; i < activeTileList.Count; i++)
        {
            Tile tile = activeTileList[i];
            float distance = Vector2.Distance(tile.transform.position, touchPosition);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                lastTile = tile;
            }
        }

        if (nearestDistance > minimumSelectionDistance || firstTile == lastTile)
        {
            lastTile = null;
            currentEntity?.ResetPosition();
            return;
        }
    }

    /// <summary>
    /// This function handles the placement of the current Entity object on the last 
    /// Tile object that the player interacted with. If the last Tile is empty, the 
    /// Entity is placed on it and the first Tile is cleared. If the last Tile is already 
    /// occupied, the function checks if the Entity occupying the Tile has the same level 
    /// as the current Entity. If they have the same level, the current Entity replaces 
    /// the previous Entity on the last Tile. If the levels are different, the function 
    /// swaps the Entities between the first and last Tiles
    /// </summary>
    private void HandleEntityPlacement()
    {
        if (lastTile)
        {
            if (!lastTile.GetIsFull())
            {
                firstTile.Clear();
                lastTile.SetEntity(currentEntity);
                currentEntity?.Deselect();
                currentEntity = null;
            }
            else
            {
                if (lastTile.GetEntity().GetEntityLevel() == currentEntity.GetEntityLevel())
                {
                    firstTile.Clear();
                    lastTile.SetEntity(currentEntity);
                    currentEntity.Deselect();
                    currentEntity = null;
                }
                else
                {
                    Entity firstTileEntity = firstTile.GetEntity();

                    firstTile.Clear();
                    firstTile.SetEntity(lastTile.GetEntity());

                    lastTile.Clear();
                    lastTile.SetEntity(firstTileEntity);
                    currentEntity.Deselect();
                    currentEntity = null;
                }
            }
        }
    }


    public void DecreseProduceTimer()
    {
        produceTimer++;
    }

    /// <summary>
    /// This function generates an Entity object and places it on a randomly selected
    /// Tile object from the active Tile list. If there are no available Tile objects,
    /// the Entity object will not be generated.
    /// </summary>
    private void ProduceEntity()
    {
        randomTileCallCount = 0;
        Tile randomTile = activeTileList.Count > 0 ? GetRandomTile() : null;
        Entity generatedEntity = randomTile ?
            Instantiate(primitiveEntityPrefab, randomTile.transform) : null;
        randomTile?.SetEntity(generatedEntity);
    }

    /// <summary>
    /// This function adds the given Tile object to the active Tile list. 
    /// If the Tile object is already in the list, it will not be added again
    /// </summary>
    /// <param name="tile"></param>
    public void AddTile(Tile tile)
    {
        if (!activeTileList.Contains(tile))
        {
            activeTileList.Add(tile);
        }
    }

    /// <summary>
    /// Returns a random empty Tile object. If the selected Tile is full,
    /// the function tries to select a different Tile by recursively calling itself. 
    /// If all Tiles are full, it returns a null value
    /// </summary>
    /// <returns></returns>
    private Tile GetRandomTile()
    {
        if (randomTileCallCount < activeTileList.Count)
        {
            Tile randomTile = activeTileList[Random.Range(0, activeTileList.Count)];
            if (randomTile.GetIsFull())
            {
                randomTileCallCount++;
                return GetRandomTile();
            }
            randomTileCallCount++;
            return randomTile;
        }
        randomTileCallCount++;
        return null;
    }
}