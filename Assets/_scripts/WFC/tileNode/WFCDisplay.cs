using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WFCDisplay : MonoBehaviour //displays the tile has been updated
{
    [SerializeField] float _collapsedTileShowUpSpeed = 0.15f;
    TileUpdationEventHandler _tileUpdationEventHandler;

    private void Awake()
    {
        _tileUpdationEventHandler = GetComponent<TileUpdationEventHandler>();
        _tileUpdationEventHandler.OnTileupdate += UpdateTileState;
    }

    void UpdateTileState(Vector2Int temp1,HashSet<string> availableTiles, Vector2Int temp2)
    {

        foreach (Transform x in transform.GetComponentInChildren<Transform>())
        {
            if (!availableTiles.Contains(x.name))
            {
                Destroy(x.gameObject);
            }
        }

        if (transform.childCount <= 0)
            Debug.LogError("Tile empty tile generation failed", gameObject);

        if (availableTiles.Count <= 1)
            tileCollapsed(availableTiles.ToList()[0]);
    }

    private void tileCollapsed(string tileName)
    {
        var selectedTile = transform.Find(tileName);

        if (selectedTile == null) Debug.LogError($"couldn't find a child named  {tileName}",gameObject);

        StartCoroutine(displayFinalTile(selectedTile));

        _tileUpdationEventHandler.TileCollapsed();
    }

    IEnumerator displayFinalTile(Transform selectedTile)
    {
        var scale = selectedTile.localScale;
        selectedTile.localPosition =Vector3.zero;
        var t = _collapsedTileShowUpSpeed;
        while(selectedTile.localScale.x < 1)
        {
            selectedTile.localScale = Vector3.Lerp(scale, Vector3.one, t);
            t += 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
