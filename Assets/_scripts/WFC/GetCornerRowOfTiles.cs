using System;
using System.Collections.Generic;
using UnityEngine;

public class GetCornerRowOfTiles : MonoBehaviour
{
    Vector2Int gridSize;
    GridData gridData;
    public enum CornerSideEnum { Top, Right, Bottom, Left };
    private void Awake()
    {
        gridData = GetComponent<GridData>();
        gridSize = gridData.GridSize;
    }

    public Tuple<List<GameObject>,int> GetRow(int _sideIntValue)
    {

        List<Vector2Int> _nodePosts = new();
        List<GameObject> _row = new();

        CornerSideEnum _side = (CornerSideEnum)_sideIntValue;

        switch (_side)
        {
            case CornerSideEnum.Right:
                _nodePosts = getNodePosts(gridSize.y,true);
                break;
            case CornerSideEnum.Left:
                _nodePosts = getNodePosts(1,true);
                break;
            case CornerSideEnum.Top:
                _nodePosts = getNodePosts(gridSize.x,false);
                break;
            case CornerSideEnum.Bottom:
                _nodePosts = getNodePosts(1,false);
                break;
        }

        foreach( Vector2Int x in _nodePosts)
        {
            GameObject _tile = gridData.GetTile(x);
            if (_tile != null)
                _row.Add(_tile);
            else
                Debug.LogError("failed to get tilefor corner row tile");
        }

        return new(_row,_sideIntValue);
    }

    List<Vector2Int> getNodePosts(int _constantValue, bool _isXConstant = false) //if x is constant y will be variable
    {
        List<Vector2Int> _nodePosts = new();
        int _constantComponent;
        if (!_isXConstant) //x is variable
        {
            _constantComponent =_constantValue;

           for(int i = 1; i < gridSize.y; i++)
            {
                _nodePosts.Add(new(i, _constantComponent));
            }

        }
        else //y is variable
        {
            _constantComponent = _constantValue;
            for (int i = 1; i < gridSize.x; i++)
            {
                _nodePosts.Add(new(_constantComponent, i));
            }
        }
        return _nodePosts;
    }
}
