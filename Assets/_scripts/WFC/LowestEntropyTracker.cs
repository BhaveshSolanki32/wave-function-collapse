using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridData),typeof(SocketCheckerTypeMatcker))]
public class LowestEntropyTracker : MonoBehaviour
{
    [SerializeField] WFCTilesData _wFCTilesDataScriptableObject;
    int _maxEntropy = 10;

    private void Awake()
    {

        _maxEntropy = _wFCTilesDataScriptableObject.ValidTilesDictionary.Count;
    }
    public Tuple<GameObject, GameObject> GetLowestEntropyTile()//return a random node with lowest entropy and random tile in it
    {
        var lowestEntropyTiles = updateEntropy();
        if (lowestEntropyTiles.Count <= 0)
        {
            return null;
        }
        var randomIndex = UnityEngine.Random.Range(0, lowestEntropyTiles.Count);
        var node = lowestEntropyTiles[randomIndex];
        lowestEntropyTiles.RemoveAt(randomIndex);
        randomIndex = UnityEngine.Random.Range(0, node.GetComponent<SuperStateTile>().CurrentTileEntropy);

       
        return new(node, node.transform.GetChild(randomIndex).gameObject);


    }

    List<GameObject> updateEntropy()
    {
        var lowestEntropyTilesList = new List<GameObject>() { };
        var lowestEntropy = _maxEntropy;
        foreach (var x in transform.GetComponentsInChildren<SuperStateTile>())
        {
            var nodeEntropy = x.CurrentTileEntropy;
            if (nodeEntropy <= lowestEntropy && nodeEntropy>1)//for nodeEntropy 1 the node is collapsed
            {
                if (nodeEntropy < lowestEntropy) lowestEntropyTilesList.Clear();

                lowestEntropyTilesList.Add(x.gameObject);
                lowestEntropy = nodeEntropy;
            }
        }
       // Debug.Log("ge" + lowestEntropyTilesList.Count+" "+lowestEntropy);
        return lowestEntropyTilesList;
    }

}
