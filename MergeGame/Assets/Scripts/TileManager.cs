using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoSingleton<TileManager>
{
    [Header("Active Tiles")]
    [SerializeField]
    private List<Tile> activeTileList;

    public List<Tile> GetActiveTileList()
    {
        return activeTileList;
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
}
