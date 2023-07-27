using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(TileUpdationEventHandler),typeof(GridNodeData))]
public class SuperStateTile : MonoBehaviour //contains data about the tile in superstate postion mainly the dict of all available tiles
{
    public int CurrentTileEntropy
    {
        get { return AvailableTileSet.Count ; }
    }
    [SerializeField] WFCTilesData wFCTilesData;
     public HashSet<string> AvailableTileSet= new();
    SocketChecker socketChecker;
    GridNodeData gridNodeData;
    enum side { up, right, down, left }; // indicates in value of ValidTilesDictionary (ie. a string array) which index code to check for adjency

    private void Awake()
    {
        AvailableTileSet = wFCTilesData.ValidTilesDictionary.Keys.ToHashSet();
        gridNodeData = GetComponent<GridNodeData>();
        socketChecker = transform.GetComponentInParent<SocketChecker>();
    }
   


    public void NeigbourUpdated(Vector2Int _neighPost, HashSet<string> _neighbourTileCodes , Vector2Int _cameFromPostBeforeThis)
    {

        

        if (AvailableTileSet.Count <= 1 && _cameFromPostBeforeThis== gridNodeData.GridPostion)
        {
            return;
        }


        HashSet<string> _tilesToRemove = new();       

        foreach (string y in AvailableTileSet)
        {
            bool _codeFound = false;
            foreach(string x in _neighbourTileCodes)
            {

                if ( socketChecker.IsMatchingSocket(x,y,_neighPost,gridNodeData.GridPostion ) )
                {
                    _codeFound = true;
                    break;
                }
            }
            if (!_codeFound) _tilesToRemove.Add(y);
        }
        if(_tilesToRemove.Count>0)
        updateSuperpostion(_tilesToRemove, _neighPost);

        if (AvailableTileSet.Count <= 0)
            Debug.LogError("Tile generation failed, " + GetComponent<GridNodeData>().GridPostion + " ran out of tiles");

    }

    private void updateSuperpostion(HashSet<string> _tilesToRemove , Vector2Int _neighPost)
    {

        foreach (string x in _tilesToRemove)
            AvailableTileSet.Remove(x);
        GetComponent<TileUpdationEventHandler>().OnTileUpdateEventTrigger(AvailableTileSet, _neighPost);

    }

  
}
