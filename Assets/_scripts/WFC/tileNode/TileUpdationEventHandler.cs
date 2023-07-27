using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridNodeData), typeof(SuperStateTile))]
public class TileUpdationEventHandler : MonoBehaviour
{
    public event Action<Vector2Int, HashSet<string>,Vector2Int> OnTileupdate;//contains functions of the nieghbours subscribed to it and local functions that occour when tile is updated // sends tiles left in superpostion to subscribed funcs
    GridNodeData gridNodeData;
    public void OnTileUpdateEventTrigger(HashSet<string> _leftTiles, Vector2Int _cameFromPost)
    {
            OnTileupdate?.Invoke(gridNodeData.GridPostion, _leftTiles, _cameFromPost);        
    }

    private void OnEnable()
    {
        gridNodeData = GetComponent<GridNodeData>();
        SubscribeToNeighboursEvent();
    }

    private void SubscribeToNeighboursEvent()
    {
        var _neighbourList = gridNodeData.NeighbourList;

        foreach (GameObject x in _neighbourList)
            OnTileupdate += x.GetComponent<SuperStateTile>().NeigbourUpdated;
    }

    public void TileCollapsed()//unsubscribe to all neighbours events
    {
        foreach (GameObject x in gridNodeData.NeighbourList)
            x.GetComponent<TileUpdationEventHandler>().OnTileupdate -= GetComponent<SuperStateTile>().NeigbourUpdated;
    }
}
