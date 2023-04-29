using UnityEngine;

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
    [SerializeField]
    private float distanceFromTop = 2;
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
        float x = (-(tileMatris.x / (2 / distanceBetweenTiles))) + (distanceBetweenTiles / 2);
        float y = (tileMatris.x / (2 / distanceBetweenTiles)) - distanceFromTop;

        for (int i = 0; i < tileMatris.y; i++)
        {
            for (int j = 0; j < tileMatris.x; j++)
            {
                Tile newTile = Instantiate(tilePrefab, tileParent);
                newTile.transform.position = new Vector3(x, y, 0);

                TileManager.singleton.AddTile(newTile);

                x += distanceBetweenTiles;
            }
            x = (-(tileMatris.x / (2 / distanceBetweenTiles))) + (distanceBetweenTiles / 2);
            y -= distanceBetweenTiles;
        }
    }
}
