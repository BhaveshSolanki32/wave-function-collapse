using System.Collections.Generic;
using UnityEngine;


public class GridNodeData : MonoBehaviour
{
    private Vector2Int _gridPosition;
    List<GameObject> _neighbourList = new List<GameObject>();
    public Vector2Int GridPosition { get { return _gridPosition; } set { _gridPosition = value; } }
    public List<GameObject> NeighbourList { get { return _neighbourList; } private set { } }

    private void Awake()
    {
        _neighbourList.AddRange(GetNeighbours.FindNeighbour(gameObject, GetComponentInParent<GridData>()));
    }
}
