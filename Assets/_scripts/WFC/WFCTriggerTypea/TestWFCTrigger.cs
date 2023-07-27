using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectCollapseTile), typeof(LowestEntropyTracker), typeof(GridData))]
public class TestWFCTrigger :WFCTrigger //collapse a single tile to check other function are working properly
{
    protected override IEnumerator startWFC()
    {
        Tuple<GameObject, GameObject> _lowestEntropyTile = GetComponent<LowestEntropyTracker>().GetLowestEntropyTile();
        if (_lowestEntropyTile == null)
        {
            Debug.LogError("collapse error using test WFC script");
            yield break;
        }
        waveFuncIsCollapsed();
        GetComponent<DirectCollapseTile>().CollapseTile(_lowestEntropyTile.Item2.name, _lowestEntropyTile.Item1);
    }
}
