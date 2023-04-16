using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TileGenerator : MonoSingleton<TileGenerator>
{
    [Header("Tile Generate")]
    [SerializeField]
    private Tile tilePrefab;
    [SerializeField]
    private Transform tileParent;
    [SerializeField]
    private Vector2Int tileMatris;

    [SerializeField]
    private float distanceBetweenTiles = 1.5f;
    private void Start()
    {
        GenerateTile();
    }

    /// <summary>
    /// Generates a grid of tiles with the given dimensions and spacing
    /// The tiles are instantiated from the provided prefab and added to the tileParent transform
    /// Each tile is positioned using x and y coordinates, which are updated after each iteration
    /// The Selector singleton is updated with each newly created tile
    /// </summary>
    private void GenerateTile()
    {
        float x = (-(tileMatris.x / (2 / distanceBetweenTiles))) + 0.5f;
        float y = ((tileMatris.x / (2 / distanceBetweenTiles))) - 1;
        for (int i = 0; i < tileMatris.x; i++)
        {
            for (int j = 0; j < tileMatris.y; j++)
            {
                Tile newTile = Instantiate(tilePrefab, tileParent);
                newTile.transform.position = new Vector3(x, y, 0);
                Selector.singleton.AddTile(newTile);
                x += distanceBetweenTiles;
            }
            x = (-(tileMatris.x / (2 / distanceBetweenTiles))) + 0.5f;
            y -= distanceBetweenTiles;
        }
    }
}
