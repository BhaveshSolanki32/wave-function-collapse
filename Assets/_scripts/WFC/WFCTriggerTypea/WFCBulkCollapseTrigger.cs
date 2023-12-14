using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RandomTileListPath), typeof(DirectCollapseTile), typeof(LowestEntropyTracker))]
public class WFCBulkCollapseTrigger : WFCTrigger//use a list (from bezier curve) to find an open path and collapse that path first used to create an open path
{
    [SerializeField] string _bulkCollapseTileCode="water";


    protected override  IEnumerator startWFC()
    {
        RandomTileListPath randomTileListPath = GetComponent<RandomTileListPath>();

        var path = randomTileListPath.GetBezierTilePath();
        foreach (var x in path)
        {
            if (x.GetComponent<SuperStateTile>().CurrentTileEntropy <= 1)
            {
                continue;
            }
            _directCollapseTile.CollapseTile(_bulkCollapseTileCode,x);
            yield return new WaitForSeconds(Delay/2);
        }
        StartCoroutine(base.startWFC());
        yield break;
    }

}
