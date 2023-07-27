using UnityEngine;

public class DirectCollapseTile : MonoBehaviour
{
    public void CollapseTile(string _tileCode, GameObject _lowestEntropyNode)
    {
        if (_lowestEntropyNode == null) Debug.LogError("_lowest Entropy node gameobject is null in DirectCollapseTile");
        TileUpdationEventHandler _tileUpdationEventHandler = _lowestEntropyNode.GetComponent<TileUpdationEventHandler>();
        _tileUpdationEventHandler.TileCollapsed();
        _lowestEntropyNode.GetComponent<SuperStateTile>().AvailableTileSet.Clear();
        _tileUpdationEventHandler.OnTileUpdateEventTrigger(new() { _tileCode }, new Vector2Int(0,0));
    }
}
