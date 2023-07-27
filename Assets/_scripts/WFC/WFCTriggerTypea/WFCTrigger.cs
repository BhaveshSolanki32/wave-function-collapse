using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(DirectCollapseTile),typeof(LowestEntropyTracker), typeof(GridData))]
public class WFCTrigger : MonoBehaviour//triggers wfc by collapsing lowest entropy tile
{
    [SerializeField] public float Delay=0.2f;
    protected LowestEntropyTracker overallEntropyManager;
    protected DirectCollapseTile directCollapseTile;
    public event Action OnWaveFunctionollapsed;

    protected void Awake()
    {
        overallEntropyManager = GetComponent<LowestEntropyTracker>();
        directCollapseTile = GetComponent<DirectCollapseTile>();
        OnWaveFunctionollapsed += waveFuncIsCollapsed;
    }

    public virtual void StartWFC()
    {
        StartCoroutine(startWFC());        
    }

    protected void waveFuncIsCollapsed()
    {
        Debug.Log("wave function is collapsed");
        StopCoroutine(startWFC());
    }

    protected virtual IEnumerator startWFC()
    {
        while (true)
        {
            Tuple<GameObject, GameObject> _lowestEntropyTile = overallEntropyManager.GetLowestEntropyTile();
            if (_lowestEntropyTile == null)
            {
                OnWaveFunctionollapsed?.Invoke();
                yield break;
            }
            directCollapseTile.CollapseTile(_lowestEntropyTile.Item2.name,_lowestEntropyTile.Item1);
            yield return new WaitForSeconds(Delay);
        }
        
    }
}
