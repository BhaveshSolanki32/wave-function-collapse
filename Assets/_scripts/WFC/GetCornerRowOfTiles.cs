using System;
using System.Collections.Generic;
using UnityEngine;

public class GetCornerRowOfTiles : MonoBehaviour
{
    Vector2Int _gridSize;
    GridData _gridData;
    enum _cornerSideEnum { Top, Right, Bottom, Left };


    private void Awake()
    {
        _gridData = GetComponent<GridData>();
        _gridSize = _gridData.GridSize;
    }

    public Tuple<List<GameObject>,int> GetRow(int sideIntValue)
    {

        var nodePosts = new List<Vector2Int>();
        var row = new List<GameObject>();

        _cornerSideEnum side = (_cornerSideEnum)sideIntValue;

        switch (side)
        {
            case _cornerSideEnum.Right:
                nodePosts = getNodePosts(_gridSize.y,true);
                break;
            case _cornerSideEnum.Left:
                nodePosts = getNodePosts(1,true);
                break;
            case _cornerSideEnum.Top:
                nodePosts = getNodePosts(_gridSize.x,false);
                break;
            case _cornerSideEnum.Bottom:
                nodePosts = getNodePosts(1,false);
                break;
        }

        foreach( var x in nodePosts)
        {
            var tile = _gridData.GetTile(x);
            if (tile != null)
                row.Add(tile);
            else
                Debug.LogError("failed to get tilefor corner row tile");
        }

        return new(row,sideIntValue);
    }

    List<Vector2Int> getNodePosts(int constantValue, bool isXConstant = false) //if x is constant y will be variable
    {
        var nodePosts = new List<Vector2Int>();
        var constantComponent=0;
        if (!isXConstant) //x is variable
        {
            constantComponent =constantValue;

           for(var i = 1; i < _gridSize.y; i++)
            {
                nodePosts.Add(new(i, constantComponent));
            }

        }
        else //y is variable
        {
            constantComponent = constantValue;
            for (var i = 1; i < _gridSize.x; i++)
            {
                nodePosts.Add(new(constantComponent, i));
            }
        }
        return nodePosts;
    }
}
