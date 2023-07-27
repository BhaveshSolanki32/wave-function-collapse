using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridData),typeof(SocketCheckerTypeMatcker))]
public class LowestEntropyTracker : MonoBehaviour
{
    int maxEntropy = 10;
    [SerializeField] WFCTilesData wFCTilesDataScriptableObject;

    private void Awake()
    {

        maxEntropy = wFCTilesDataScriptableObject.ValidTilesDictionary.Count;
    }
    public Tuple<GameObject, GameObject> GetLowestEntropyTile()//return a random node with lowest entropy and random tile in it
    {
        List<GameObject> lowestEntropyTiles = updateEntropy();
        if (lowestEntropyTiles.Count <= 0)
        {
            return null;
        }
        int _randomIndex = UnityEngine.Random.Range(0, lowestEntropyTiles.Count);
        GameObject _node = lowestEntropyTiles[_randomIndex];
        lowestEntropyTiles.RemoveAt(_randomIndex);
        _randomIndex = UnityEngine.Random.Range(0, _node.GetComponent<SuperStateTile>().CurrentTileEntropy);

       
        return new(_node, _node.transform.GetChild(_randomIndex).gameObject);


    }

    List<GameObject> updateEntropy()
    {
        List<GameObject> _lowestEntropyTilesList = new() { };
        int _lowestEntropy = maxEntropy;
        foreach (SuperStateTile x in transform.GetComponentsInChildren<SuperStateTile>())
        {
            int _nodeEntropy = x.CurrentTileEntropy;
            if (_nodeEntropy <= _lowestEntropy && _nodeEntropy>1)//for _nodeEntropy 1 the node is collapsed
            {
                if (_nodeEntropy < _lowestEntropy) _lowestEntropyTilesList.Clear();

                _lowestEntropyTilesList.Add(x.gameObject);
                _lowestEntropy = _nodeEntropy;
            }
        }
       // Debug.Log("ge" + _lowestEntropyTilesList.Count+" "+_lowestEntropy);
        return _lowestEntropyTilesList;
    }

}
