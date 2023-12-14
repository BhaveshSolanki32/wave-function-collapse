using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DirectCollapseTile), typeof(LowestEntropyTracker), typeof(GridData))]
public class WFCTrigger : MonoBehaviour//triggers wfc by collapsing lowest entropy tile
{
    float _delay = 0.2f;
    protected LowestEntropyTracker _overallEntropyManager;
    protected DirectCollapseTile _directCollapseTile;
    public float Delay { get { return _delay; } set { _delay = value; } }
    public event Action OnWaveFunctionCollapsed;

    protected void Awake()
    {
        _overallEntropyManager = GetComponent<LowestEntropyTracker>();
        _directCollapseTile = GetComponent<DirectCollapseTile>();
        OnWaveFunctionCollapsed += waveFuncIsCollapsed;
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
            var lowestEntropyTile = _overallEntropyManager.GetLowestEntropyTile();
            if (lowestEntropyTile == null)
            {
                OnWaveFunctionCollapsed?.Invoke();
                yield break;
            }
            _directCollapseTile.CollapseTile(lowestEntropyTile.Item2.name, lowestEntropyTile.Item1);
            yield return new WaitForSeconds(_delay);
        }

    }
}
