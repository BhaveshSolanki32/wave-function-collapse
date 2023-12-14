using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(TileUpdationEventHandler),typeof(GridNodeData))]
public class SuperStateTile : MonoBehaviour //contains data about the tile in superstate postion mainly the dict of all available tiles
{
    
    [SerializeField] WFCTilesData _wFCTilesData;
    SocketChecker _socketChecker;
    GridNodeData _gridNodeData;
    enum _side { up, right, down, left }; // indicates in value of ValidTilesDictionary (ie. a string array) which index code to check for adjency
    HashSet<string> _availableTileSet = new();

    public HashSet<string> AvailableTileSet { get { return _availableTileSet; } set { _availableTileSet = value; } }
    public int CurrentTileEntropy
    {
        get { return _availableTileSet.Count; }
        private set { }
    }


    private void Awake()
    {
        _availableTileSet = _wFCTilesData.ValidTilesDictionary.Keys.ToHashSet();
        _gridNodeData = GetComponent<GridNodeData>();
        _socketChecker = transform.GetComponentInParent<SocketChecker>();
    }
   


    public void NeigbourUpdated(Vector2Int neighPost, HashSet<string> neighbourTileCodes , Vector2Int cameFromPostBeforeThis)
    {

        

        if (_availableTileSet.Count <= 1 && cameFromPostBeforeThis== _gridNodeData.GridPosition)
        {
            return;
        }


        var tilesToRemove = new HashSet<string>();       

        foreach (var y in _availableTileSet)
        {
            var codeFound = false;
            foreach(var x in neighbourTileCodes)
            {

                if ( _socketChecker.IsMatchingSocket(x,y,neighPost,_gridNodeData.GridPosition ) )
                {
                    codeFound = true;
                    break;
                }
            }
            if (!codeFound) tilesToRemove.Add(y);
        }
        if(tilesToRemove.Count>0)
        updateSuperpostion(tilesToRemove, neighPost);

        if (_availableTileSet.Count <= 0)
            Debug.LogError("Tile generation failed, " + GetComponent<GridNodeData>().GridPosition + " ran out of tiles");

    }

    private void updateSuperpostion(HashSet<string> tilesToRemove , Vector2Int neighPost)
    {

        foreach (var x in tilesToRemove)
            _availableTileSet.Remove(x);
        GetComponent<TileUpdationEventHandler>().OnTileUpdateEventTrigger(_availableTileSet, neighPost);

    }

  
}
