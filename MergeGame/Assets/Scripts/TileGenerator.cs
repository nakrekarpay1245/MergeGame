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
    private Vector2 tileMatris;

    [SerializeField]
    private float distanceBetweenTiles = 1.5f;
    private void Start()
    {
        GenerateTile();
    }

    private void GenerateTile()
    {
        float x = -(tileMatris.x / (2 / distanceBetweenTiles));
        float y = (tileMatris.x / (2 / distanceBetweenTiles));
        for (int i = 0; i < tileMatris.x; i++)
        {
            for (int j = 0; j < tileMatris.y; j++)
            {
                Tile newTile = Instantiate(tilePrefab, tileParent);
                newTile.transform.position = new Vector3(x, y, 0);
                Selector.singleton.AddTile(newTile);
                x += distanceBetweenTiles;
            }
            x = -(tileMatris.x / (2 / distanceBetweenTiles));
            y -= distanceBetweenTiles;
        }
    }
}
