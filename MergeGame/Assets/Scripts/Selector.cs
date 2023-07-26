using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Selector : MonoSingleton<Selector>
{
    private const float _minimumSelectionDistance = 0.75f;

    private Tile _firstTile = null;
    private Tile _lastTile = null;

    [Header("Entity")]
    [SerializeField]
    private Entity _primitiveEntityPrefab = null;
    private Entity _currentEntity = null;

    private int _randomTileCallCount;

    [Header("Produce")]
    [SerializeField]
    private Image _produceButtonFill;

    [Header("Auto Produce")]
    [SerializeField]
    private float _autoProduceTime = 10;
    private float _produceTimer;

    [Header("Deliver")]
    [SerializeField]
    private GameObject _deliverButton;

    private Touch _touch;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Began)
            {
                SelectFirstTileEntity();
            }
            else if (_touch.phase == TouchPhase.Moved)
            {
                MoveCurrentEntity();
            }
            else if (_touch.phase == TouchPhase.Ended)
            {
                SelectLastTile();
                if (_lastTile && _firstTile && _currentEntity)
                {
                    HandleEntityPlacement();
                }
                _firstTile = null;
                _lastTile = null;
                _currentEntity = null;
            }
        }
        else
        {
            _currentEntity?.ResetPosition();
        }

        CalculateProduceTimer();
    }

    /// <summary>
    /// Calculates the produce timer and checks if it's time to produce a new entity
    /// </summary>
    private void CalculateProduceTimer()
    {
        if (_produceTimer >= _autoProduceTime)
        {
            _produceTimer = 0;
            ProduceEntity();
        }
        else
        {
            _produceTimer += Time.deltaTime;
        }

        DisplayProduceTimer();
    }

    /// <summary>
    /// Decreases the produce timer by 1
    /// </summary>
    public void IncreaseProduceTimer()
    {
        _produceTimer++;
    }

    /// <summary>
    /// Displays the produce timer by updating the fill amount of the produce button fill image.
    /// </summary>
    private void DisplayProduceTimer()
    {
        _produceButtonFill.fillAmount = _produceTimer / _autoProduceTime;
    }

    /// <summary>
    /// Selects the first tile that the user clicks on and sets it as the starting 
    /// point of a move operation.If the user's click is too far from any tiles, no 
    /// tile is selected. If the selected tile is empty or has no entity, no entity 
    /// is selected for the move operation.
    /// </summary>
    private void SelectFirstTileEntity()
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(_touch.position);
        float nearestDistance = float.MaxValue;

        for (int i = 0; i < TileManager.singleton.ActiveTileList.Count; i++)
        {
            Tile tile = TileManager.singleton.ActiveTileList[i];
            float distance = Vector2.Distance(tile.transform.position, touchPosition);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                _firstTile = tile;
            }
        }

        if (nearestDistance > _minimumSelectionDistance)
        {
            _firstTile = null;
            return;
        }

        _currentEntity = _firstTile.Entity;
        _currentEntity?.Select();
    }

    /// <summary>
    /// This function moves the current Entity object to the position of the mouse cursor 
    /// on the screen. If there is no current Entity, this function does nothing.
    /// </summary>
    private void MoveCurrentEntity()
    {
        if (_currentEntity)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(_touch.position);
            Vector2 entityPosition = new Vector3(touchPosition.x, touchPosition.y, 0);
            _currentEntity.transform.position = entityPosition;
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
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(_touch.position);
        float nearestDistance = float.MaxValue;

        for (int i = 0; i < TileManager.singleton.ActiveTileList.Count; i++)
        {
            Tile tile = TileManager.singleton.ActiveTileList[i];
            float distance = Vector2.Distance(tile.transform.position, touchPosition);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                _lastTile = tile;
            }
        }

        if (nearestDistance > _minimumSelectionDistance || _firstTile == _lastTile)
        {
            _lastTile = null;
            _currentEntity?.ResetPosition();
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
        if (_lastTile)
        {
            if (!_lastTile.IsFull)
            {
                _firstTile.Clear();
                _lastTile.Entity = _currentEntity;
                _currentEntity?.DeSelect();
                _currentEntity = null;

                AudioManager.singleton.PlaySound("Change");
            }
            else
            {
                if (_lastTile.Entity.EntityLevel == _currentEntity.EntityLevel)
                {
                    _firstTile.Clear();
                    _lastTile.Entity = _currentEntity;
                    _currentEntity.DeSelect();
                    _currentEntity = null;

                    ParticleManager.singleton.PlayParticleAtPoint(_lastTile.transform.position);

                    AudioManager.singleton.PlaySound("Sparkle");

                    RequestManager.singleton.ControlRequestButton();
                }
                else
                {
                    Entity firstTileEntity = _firstTile.Entity;

                    _firstTile.Clear();
                    _firstTile.Entity = _lastTile.Entity;

                    _lastTile.Clear();
                    _lastTile.Entity = firstTileEntity;
                    _currentEntity.DeSelect();
                    _currentEntity = null;

                    AudioManager.singleton.PlaySound("Change");
                }
            }
        }
    }

    /// <summary>
    /// This function generates an Entity object and places it on a randomly selected
    /// Tile object from the active Tile list. If there are no available Tile objects,
    /// the Entity object will not be generated.
    /// </summary>
    public void ProduceEntity()
    {
        _randomTileCallCount = 0;

        Tile randomTile = GetRandomTile();
        if (!randomTile)
        {
            return;
        }

        Entity generatedEntity = randomTile ?
            Instantiate(_primitiveEntityPrefab, randomTile.transform) : null;
        randomTile.Entity = generatedEntity;

        ParticleManager.singleton.PlayParticleAtPoint(randomTile.transform.position);
        AudioManager.singleton.PlaySound("Pop");

        RequestManager.singleton.ControlRequestButton();
    }

    /// <summary>
    /// Returns a random empty Tile object. If the selected Tile is full,
    /// the function tries to select a different Tile by recursively calling itself. 
    /// If all Tiles are full, it returns a null value
    /// </summary>
    /// <returns></returns>
    private Tile GetRandomTile()
    {
        if (_randomTileCallCount < TileManager.singleton.ActiveTileList.Count)
        {
            Tile randomTile = TileManager.singleton.ActiveTileList[Random.Range(0, TileManager.singleton.ActiveTileList.Count)];
            if (randomTile.IsFull)
            {
                _randomTileCallCount++;
                return GetRandomTile();
            }
            _randomTileCallCount++;
            return randomTile;
        }
        _randomTileCallCount++;
        return null;
    }
}