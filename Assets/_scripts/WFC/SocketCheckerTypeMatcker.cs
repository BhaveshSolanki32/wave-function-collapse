using UnityEngine;

public class SocketCheckerTypeMatcker : SocketChecker//have extended test for socket check by matcking diff codes colors
{
    public override bool IsMatchingSocket(string _neighbourNodeCode, string _currentNodeCode, Vector2Int _neighbourPost, Vector2Int _currentPost)
    {
        bool _isMatch = false;

        string _neighboursSocketCode = getSocketCode(_neighbourNodeCode, sideFinder(_neighbourPost, _currentPost));
        string _socketCode = getSocketCode(_currentNodeCode, sideFinder(_neighbourPost, _currentPost, true));

        string _gCode = wFCTilesDataScriptableObject.ValidTilesDictionary["sU"][0];
        string _waterCode = wFCTilesDataScriptableObject.ValidTilesDictionary["water"][0];

        if ((_socketCode == _gCode && _neighboursSocketCode == _waterCode) || (_neighboursSocketCode == _gCode && _socketCode == _waterCode))
            _isMatch = true;
        else if (_socketCode == _gCode && _neighboursSocketCode == _gCode)
            _isMatch = false;
        else if (_neighboursSocketCode == _socketCode)
            _isMatch = true;


        return _isMatch;
    }
}
