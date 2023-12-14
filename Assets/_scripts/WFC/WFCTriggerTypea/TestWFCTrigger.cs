using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectCollapseTile), typeof(LowestEntropyTracker), typeof(GridData))]
public class TestWFCTrigger :WFCTrigger //collapse a single tile to check other function are working properly
{
    protected override IEnumerator startWFC()
    {
        var lowestEntropyTile = GetComponent<LowestEntropyTracker>().GetLowestEntropyTile();
        if (lowestEntropyTile == null)
        {
            Debug.LogError("collapse error using test WFC script");
            yield break;
        }
        waveFuncIsCollapsed();
        GetComponent<DirectCollapseTile>().CollapseTile(lowestEntropyTile.Item2.name, lowestEntropyTile.Item1);
    }
}
