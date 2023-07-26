using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoSingleton<TileManager>
{
    [Header("Active Tiles")]
    [SerializeField]
    private List<Tile> _activeTileList;

    public List<Tile> ActiveTileList { get => _activeTileList; private set => _activeTileList = value; }

    /// <summary>
    /// This function adds the given Tile object to the active Tile list. 
    /// If the Tile object is already in the list, it will not be added again
    /// </summary>
    /// <param name="tile"></param>
    public void AddTile(Tile tile)
    {
        if (!_activeTileList.Contains(tile))
        {
            _activeTileList.Add(tile);
        }
    }
}