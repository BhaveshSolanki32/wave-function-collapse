using UnityEngine;

public class DirectCollapseTile : MonoBehaviour
{
    public void CollapseTile(string tileCode, GameObject lowestEntropyNode)
    {
        if (lowestEntropyNode == null) Debug.LogError("_lowest Entropy node gameobject is null in DirectCollapseTile");
        var _tileUpdationEventHandler = lowestEntropyNode.GetComponent<TileUpdationEventHandler>();
        _tileUpdationEventHandler.TileCollapsed();
        lowestEntropyNode.GetComponent<SuperStateTile>().AvailableTileSet.Clear();
        _tileUpdationEventHandler.OnTileUpdateEventTrigger(new() { tileCode }, new Vector2Int(0,0));
    }
}
