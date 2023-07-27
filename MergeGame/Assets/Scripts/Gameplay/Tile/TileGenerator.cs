using UnityEngine;

public class TileGenerator : MonoSingleton<TileGenerator>
{
    [Header("Tile Generate")]
    [SerializeField]
    private Tile _tilePrefab;
    [SerializeField]
    private Transform _tileParent;
    [SerializeField]
    private Vector2Int _tileMatris;

    [SerializeField]
    private float _distanceBetweenTiles = 1.5f;
    [SerializeField]
    private float _distanceFromTop = 2;
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
        float x = (-(_tileMatris.x / (2 / _distanceBetweenTiles))) + (_distanceBetweenTiles / 2);
        float y = (_tileMatris.x / (2 / _distanceBetweenTiles)) - _distanceFromTop;

        for (int i = 0; i < _tileMatris.y; i++)
        {
            for (int j = 0; j < _tileMatris.x; j++)
            {
                Tile newTile = Instantiate(_tilePrefab, _tileParent);
                newTile.transform.position = new Vector3(x, y, 0);

                TileManager.singleton.AddTile(newTile);

                x += _distanceBetweenTiles;
            }
            x = (-(_tileMatris.x / (2 / _distanceBetweenTiles))) + (_distanceBetweenTiles / 2);
            y -= _distanceBetweenTiles;
        }
    }
}