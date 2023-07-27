using System;
using System.Collections.Generic;
using UnityEngine;

public class RandomTileListPath : MonoBehaviour //returns list of gameobject using beizier curve;
{
    GetCornerRowOfTiles getCornerRowOfTiles;
    [SerializeField] float curvatureStrength = 4;
    [SerializeField] float smoothness = 0.01f;
    GridData gridData;
    public enum CornerSideEnum { Top, Right, Bottom, Left, Random };


    public List<GameObject> GetBezierTilePath()
    {
        List<GameObject> _bezierPathGameObject = new();


        Tuple<GameObject, GameObject> _chosenNode = getRandomGameObjectsFromCorner();


        Vector3 _chosenEndNodePostion = _chosenNode.Item1.transform.position;
        Vector3 _chosenFirstNodePostion = _chosenNode.Item2.transform.position;

        List<Vector3> _bezierPathPosts = BezeirPath.s_GetBezierPath(_chosenFirstNodePostion, randomVector(), randomVector(), _chosenEndNodePostion, smoothness);

        for (int i = 1; i < _bezierPathPosts.Count; i++)
        {
             
            Debug.DrawLine(bringInLimit( _bezierPathPosts[i - 1]), bringInLimit( _bezierPathPosts[i]), Color.red, 100);

            _bezierPathPosts[i] = bringInLimit(_bezierPathPosts[i]);

            Vector2Int _tilePost;

            _tilePost = WorldToGridPostion.Convert(_bezierPathPosts[i],gridData);

            _bezierPathGameObject.Add(gridData.GetTile(_tilePost));
        }


        return _bezierPathGameObject;
    }

    Vector3 bringInLimit(Vector3 _post)
    {
        Vector3 _farthestTilePost = gridData.GetTile(new(gridData.GridSize.x, gridData.GridSize.y)).transform.position;
        Vector3 _closestTilePost = gridData.GetTile(new(1, 1)).transform.position;
       
        _post.x = Mathf.Clamp(_post.x, _closestTilePost.x, _farthestTilePost.x);
        _post.y= Mathf.Clamp(_post.y, _closestTilePost.y, _farthestTilePost.y);
        _post.z = 0;
        return _post;
    }

    private void Awake()
    {
        getCornerRowOfTiles = GetComponent<GetCornerRowOfTiles>();
        gridData = GetComponent<GridData>();
    }

    Vector3 randomVector()
    {
        Vector3 _rand = new Vector3((UnityEngine.Random.insideUnitCircle.x) * curvatureStrength, (UnityEngine.Random.insideUnitCircle.y) * curvatureStrength, 0);
        return _rand + transform.position;
    }

    Tuple<GameObject, GameObject> getRandomGameObjectsFromCorner(CornerSideEnum _side = CornerSideEnum.Random) //item 1 chosen gameobject item 2 it's sides
    {

        CornerSideEnum _otherSide;
        if (_side == CornerSideEnum.Random)
        {
            _side = (CornerSideEnum)UnityEngine.Random.Range(0, 4);
        }
        _otherSide = ((int)_side >= 2) ? ((CornerSideEnum)((int)_side - 2)) : (((CornerSideEnum)((int)_side + 2)));

        GameObject _firstTile = getRandomNode(_side);
        GameObject _lastTile = getRandomNode(_otherSide);

        return new(_firstTile, _lastTile);
    }


    GameObject getRandomNode(CornerSideEnum _side)
    {
        Vector2Int _gridSize = gridData.GridSize;

        GameObject _node=null;
        switch (_side)
        {
            case CornerSideEnum.Right:
                _node = getNodePosts(_gridSize.x, true);
                break;
            case CornerSideEnum.Left:
                _node = getNodePosts(1, true);
                break;
            case CornerSideEnum.Top:
                _node = getNodePosts(_gridSize.y, false);
                break;
            case CornerSideEnum.Bottom:
                _node = getNodePosts(1, false);
                break;
             default:
                Debug.LogError("Wrong side provided in getRandomNode");
                break;
        }
        return _node;
    }

    GameObject getNodePosts(int _constantValue, bool _isXConstant = false) //if x is constant y will be variable
    {
        GameObject _node = null;
        if (_isXConstant)
        {
            _node = gridData.GetTile(new(_constantValue, UnityEngine.Random.Range(2, gridData.GridSize.y)));
        }
        else
        {
            _node = gridData.GetTile(new(UnityEngine.Random.Range(2, gridData.GridSize.x),_constantValue));
        }
        if (_node == null) Debug.LogError("failed to find node in getRandomNode");
        return _node;
    }
}
