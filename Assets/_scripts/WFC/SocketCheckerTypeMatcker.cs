using UnityEngine;

public class SocketCheckerTypeMatcker : SocketChecker//have extended test for socket check by matcking diff codes colors
{
    public override bool IsMatchingSocket(string neighbourNodeCode, string currentNodeCode, Vector2Int neighbourPost, Vector2Int currentPost)
    {
        var isMatch = false;

        var neighboursSocketCode = getSocketCode(neighbourNodeCode, sideFinder(neighbourPost, currentPost));
        var socketCode = getSocketCode(currentNodeCode, sideFinder(neighbourPost, currentPost, true));

        var gCode = _wFCTilesDataScriptableObject.ValidTilesDictionary["sU"][0];
        var waterCode = _wFCTilesDataScriptableObject.ValidTilesDictionary["water"][0];

        if ((socketCode == gCode && neighboursSocketCode == waterCode) || (neighboursSocketCode == gCode && socketCode == waterCode))
            isMatch = true;
        else if (socketCode == gCode && neighboursSocketCode == gCode)
            isMatch = false;
        else if (neighboursSocketCode == socketCode)
            isMatch = true;


        return isMatch;
    }
}
