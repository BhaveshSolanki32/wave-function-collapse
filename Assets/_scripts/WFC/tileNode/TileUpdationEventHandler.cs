using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridNodeData), typeof(SuperStateTile))]
public class TileUpdationEventHandler : MonoBehaviour
{
    GridNodeData _gridNodeData;
    public event Action<Vector2Int, HashSet<string>, Vector2Int> OnTileupdate;//contains functions of the nieghbours subscribed to it and local functions that occour when tile is updated // sends tiles left in superpostion to subscribed funcs

    private void OnEnable()
    {
        _gridNodeData = GetComponent<GridNodeData>();
        SubscribeToNeighboursEvent();
    }

    public void OnTileUpdateEventTrigger(HashSet<string> leftTiles, Vector2Int cameFromPost)
    {
        OnTileupdate?.Invoke(_gridNodeData.GridPosition, leftTiles, cameFromPost);
    }

    public void TileCollapsed()//unsubscribe to all neighbours events
    {
        foreach (var x in _gridNodeData.NeighbourList)
            x.GetComponent<TileUpdationEventHandler>().OnTileupdate -= GetComponent<SuperStateTile>().NeigbourUpdated;
    }

    private void SubscribeToNeighboursEvent()
    {
        var neighbourList = _gridNodeData.NeighbourList;

        foreach (var x in neighbourList)
            OnTileupdate += x.GetComponent<SuperStateTile>().NeigbourUpdated;
    }

    
}
