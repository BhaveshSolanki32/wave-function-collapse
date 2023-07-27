using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketChecker : MonoBehaviour
{
    [SerializeField] protected WFCTilesData wFCTilesDataScriptableObject;
    protected enum side { up, right, down, left }; // indicates in value of ValidTilesDictionary (ie. a string array) which index code to check for adjency

    public virtual bool IsMatchingSocket(string _neighbourNodeCode, string _currentNodeCode,Vector2Int _neighbourPost, Vector2Int _currentPost)
    {
        string _neighboursSocketCode = getSocketCode(_neighbourNodeCode, sideFinder(_neighbourPost,_currentPost));
        string _socketCode = getSocketCode(_currentNodeCode, sideFinder(_neighbourPost,_currentPost,true));

        return _neighboursSocketCode == _socketCode;
    }

    protected virtual string getSocketCode(string _name, side _side)
    {
        if (wFCTilesDataScriptableObject != null)
            return wFCTilesDataScriptableObject.ValidTilesDictionary[_name][(int)_side];
        else
        {
            Debug.LogError("tilesDataScriptableObject is null in SuperPostionStateHandler");
            return "";
        }
    }

    protected virtual side sideFinder(Vector2Int _neighPost, Vector2Int _currentPost, bool _inverse = false) //the side of the tile from which the neighbour was attached to // inverse to return the opposite side
    {

        Vector2Int _tilePostDiff = _neighPost -_currentPost;

        if (_tilePostDiff == new Vector2Int(1, 0))
        {
            return (_inverse) ? (side.right) : (side.left);
        }
        else if (_tilePostDiff == new Vector2Int(-1, 0))
        {
            return (!_inverse) ? (side.right) : (side.left);
        }
        else if (_tilePostDiff == new Vector2Int(0, 1))
        {
            return (_inverse) ? (side.up) : (side.down);
        }
        else if (_tilePostDiff == new Vector2Int(0, -1))
        {
            return (!_inverse) ? (side.up) : (side.down);
        }
        else
        {
            Debug.LogError("side not found ");
        }

        return side.down;
    }
}
