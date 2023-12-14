using UnityEngine;

public class GridData : MonoBehaviour
{
    Vector2Int _gridSize;
    float _cellSize;
    public float CellSize { get { return _cellSize; } set { _cellSize = value; } }
    public Vector2Int GridSize { get { return _gridSize; } set { _gridSize = value; } }


    public GameObject GetTile(Vector2Int post)
    {
        var index = (post.x - 1) * _gridSize.y + post.y - 1;

        if (index > transform.childCount - 1 || index < 0 || post.x<=0||post.y<=0 || post.x>_gridSize.x || post.y>_gridSize.y)
            return null;
        else
        {
            var tileNode = transform.GetChild(index).gameObject;
            if (tileNode.GetComponent<GridNodeData>().GridPosition != post) Debug.LogError($"wrong tile sent asked for {post} returned = {tileNode.GetComponent<GridNodeData>().GridPosition}",tileNode);
            return tileNode;
        }
            
    }
}
