using UnityEngine;

public class GridData : MonoBehaviour
{
    public Vector2Int GridSize;
    public float CellSize;
    public GameObject GetTile(Vector2Int post)
    {
        int _index = (post.x - 1) * GridSize.y + post.y - 1;

        if (_index > transform.childCount - 1 || _index < 0 || post.x<=0||post.y<=0 || post.x>GridSize.x || post.y>GridSize.y)
            return null;
        else
        {
            GameObject _tileNode = transform.GetChild(_index).gameObject;
            if (_tileNode.GetComponent<GridNodeData>().GridPostion != post) Debug.LogError("wrong tile sent asked for "+post+"returned = "+ _tileNode.GetComponent<GridNodeData>().GridPostion,_tileNode);
            return _tileNode;
        }
            
    }
}
