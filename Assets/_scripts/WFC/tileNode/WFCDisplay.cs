using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WFCDisplay : MonoBehaviour //displays the tile has been updated
{
    TileUpdationEventHandler tileUpdationEventHandler;
    [SerializeField] float collapsedTileShowUpSpeed = 0.15f;
    private void Awake()
    {
        tileUpdationEventHandler = GetComponent<TileUpdationEventHandler>();
        tileUpdationEventHandler.OnTileupdate += UpdateTileState;
    }

    void UpdateTileState(Vector2Int _temp1,HashSet<string> _availableTiles, Vector2Int _temp2)
    {

        foreach (Transform x in transform.GetComponentInChildren<Transform>())
        {
            if (!_availableTiles.Contains(x.name))
            {
                Destroy(x.gameObject);
            }
        }

        if (transform.childCount <= 0)
            Debug.LogError("Tile empty tile generation failed", gameObject);

        if (_availableTiles.Count <= 1)
            tileCollapsed(_availableTiles.ToList()[0]);
    }

    private void tileCollapsed(string _tileName)
    {
        Transform _selectedTile = transform.Find(_tileName);

        if (_selectedTile == null) Debug.LogError("couldn't find a child named " + _tileName,gameObject);

        StartCoroutine(displayFinalTile(_selectedTile));

        tileUpdationEventHandler.TileCollapsed();
    }

    IEnumerator displayFinalTile(Transform _selectedTile)
    {
        Vector3 _scale = _selectedTile.localScale;
        _selectedTile.localPosition =Vector3.zero;
        float t = collapsedTileShowUpSpeed;
        while(_selectedTile.localScale.x < 1)
        {
            _selectedTile.localScale = Vector3.Lerp(_scale, Vector3.one, t);
            t += 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
