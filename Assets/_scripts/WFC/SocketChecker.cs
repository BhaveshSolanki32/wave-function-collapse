using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketChecker : MonoBehaviour
{
    [SerializeField] protected WFCTilesData _wFCTilesDataScriptableObject;
    protected enum _side { up, right, down, left }; // indicates in value of ValidTilesDictionary (ie. a string array) which index code to check for adjency

    public virtual bool IsMatchingSocket(string neighbourNodeCode, string currentNodeCode,Vector2Int neighbourPost, Vector2Int currentPost)
    {
        var neighboursSocketCode = getSocketCode(neighbourNodeCode, sideFinder(neighbourPost,currentPost));
        var socketCode = getSocketCode(currentNodeCode, sideFinder(neighbourPost,currentPost,true));

        return neighboursSocketCode == socketCode;
    }

    protected virtual string getSocketCode(string name, _side side)
    {
        if (_wFCTilesDataScriptableObject != null)
            return _wFCTilesDataScriptableObject.ValidTilesDictionary[name][(int)side];
        else
        {
            Debug.LogError("tilesDataScriptableObject is null in SuperPostionStateHandler");
            return "";
        }
    }

    protected virtual _side sideFinder(Vector2Int neighPost, Vector2Int currentPost, bool inverse = false) //the side of the tile from which the neighbour was attached to // inverse to return the opposite side
    {

        var tilePostDiff = neighPost -currentPost;

        if (tilePostDiff == new Vector2Int(1, 0))
        {
            return (inverse) ? (_side.right) : (_side.left);
        }
        else if (tilePostDiff == new Vector2Int(-1, 0))
        {
            return (!inverse) ? (_side.right) : (_side.left);
        }
        else if (tilePostDiff == new Vector2Int(0, 1))
        {
            return (inverse) ? (_side.up) : (_side.down);
        }
        else if (tilePostDiff == new Vector2Int(0, -1))
        {
            return (!inverse) ? (_side.up) : (_side.down);
        }
        else
        {
            Debug.LogError("side not found ");
        }

        return _side.down;
    }
}
