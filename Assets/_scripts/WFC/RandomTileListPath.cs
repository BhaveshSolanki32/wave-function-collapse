using System;
using System.Collections.Generic;
using UnityEngine;

public class RandomTileListPath : MonoBehaviour //returns list of gameobject using beizier curve;
{
    [SerializeField] float _curvatureStrength = 4;
    [SerializeField] float _smoothness = 0.01f;
    GridData _gridData;
    enum _cornerSideEnum { Top, Right, Bottom, Left, Random };
    GetCornerRowOfTiles _getCornerRowOfTiles;


    private void Awake()
    {
        _getCornerRowOfTiles = GetComponent<GetCornerRowOfTiles>();
        _gridData = GetComponent<GridData>();
    }

    public List<GameObject> GetBezierTilePath()
    {
        var bezierPathGameObject = new List<GameObject>();


        var chosenNode = getRandomGameObjectsFromCorner();


        var chosenEndNodePostion = chosenNode.Item1.transform.position;
        var chosenFirstNodePostion = chosenNode.Item2.transform.position;

        var bezierPathPosts = BezeirPath.GetBezierPath(chosenFirstNodePostion, randomVector(), randomVector(), chosenEndNodePostion, _smoothness);

        for (var i = 1; i < bezierPathPosts.Count; i++)
        {
             
            Debug.DrawLine(bringInLimit( bezierPathPosts[i - 1]), bringInLimit( bezierPathPosts[i]), Color.red, 100);

            bezierPathPosts[i] = bringInLimit(bezierPathPosts[i]);

            Vector2Int tilePost;

            tilePost = WorldToGridPostion.Convert(bezierPathPosts[i],_gridData);

            bezierPathGameObject.Add(_gridData.GetTile(tilePost));
        }


        return bezierPathGameObject;
    }

    Vector3 bringInLimit(Vector3 post)
    {
        var farthestTilePost = _gridData.GetTile(new(_gridData.GridSize.x, _gridData.GridSize.y)).transform.position;
        var closestTilePost = _gridData.GetTile(new(1, 1)).transform.position;
       
        post.x = Mathf.Clamp(post.x, closestTilePost.x, farthestTilePost.x);
        post.y= Mathf.Clamp(post.y, closestTilePost.y, farthestTilePost.y);
        post.z = 0;
        return post;
    }

    Vector3 randomVector()
    {
        var rand = new Vector3((UnityEngine.Random.insideUnitCircle.x) * _curvatureStrength, (UnityEngine.Random.insideUnitCircle.y) * _curvatureStrength, 0);
        return rand + transform.position;
    }

    Tuple<GameObject, GameObject> getRandomGameObjectsFromCorner(_cornerSideEnum side = _cornerSideEnum.Random) //item 1 chosen gameobject item 2 it's sides
    {

        _cornerSideEnum otherSide;
        if (side == _cornerSideEnum.Random)
        {
            side = (_cornerSideEnum)UnityEngine.Random.Range(0, 4);
        }
        otherSide = ((int)side >= 2) ? ((_cornerSideEnum)((int)side - 2)) : (((_cornerSideEnum)((int)side + 2)));

        var firstTile = getRandomNode(side);
        var lastTile = getRandomNode(otherSide);

        return new(firstTile, lastTile);
    }


    GameObject getRandomNode(_cornerSideEnum side)
    {
        var gridSize = _gridData.GridSize;

        GameObject node=null;
        switch (side)
        {
            case _cornerSideEnum.Right:
                node = getNodePosts(gridSize.x, true);
                break;
            case _cornerSideEnum.Left:
                node = getNodePosts(1, true);
                break;
            case _cornerSideEnum.Top:
                node = getNodePosts(gridSize.y, false);
                break;
            case _cornerSideEnum.Bottom:
                node = getNodePosts(1, false);
                break;
             default:
                Debug.LogError("Wrong side provided in getRandomNode");
                break;
        }
        return node;
    }

    GameObject getNodePosts(int constantValue, bool isXConstant = false) //if x is constant y will be variable
    {
        GameObject node = null;
        if (isXConstant)
        {
            node = _gridData.GetTile(new(constantValue, UnityEngine.Random.Range(2, _gridData.GridSize.y)));
        }
        else
        {
            node = _gridData.GetTile(new(UnityEngine.Random.Range(2, _gridData.GridSize.x),constantValue));
        }
        if (node == null) Debug.LogError("failed to find node in getRandomNode");
        return node;
    }
}
