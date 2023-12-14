using UnityEngine;

public class GridGenerator : MonoBehaviour // generates a grid based on given parameters
{
    [SerializeField] int _height;
    [SerializeField] int _width;
    [SerializeField] GameObject _placeHolder;
    [SerializeField] float _cellSize;
    public void CreateGrid()
    {
        var tiles = new GameObject();
        tiles.name = "tiles";
        GridData gridData = tiles.AddComponent<GridData>();
        for (var i = 1; i <= _width; i++)
        {
            for (var j = 1; j <= _height; j++)
            {
                var tile = Instantiate(_placeHolder, new Vector3(i, j, 0) * _cellSize, _placeHolder.transform.rotation);
                tile.transform.SetParent(tiles.transform);
                tile.GetComponent<GridNodeData>().GridPosition = new(i, j);
            }
        }
        gridData.GridSize = new(_width, _height);
        gridData.CellSize = _cellSize;
        tiles.transform.position = new Vector3(_width + 1, _height + 1, 0) * -1 * _cellSize / 2;
    }
}

