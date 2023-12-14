using System.Collections.Generic;
using UnityEngine;

public static class GetNeighbours
{
    static List<GameObject> s_neighbourList = new() { };

    public static List<GameObject> FindNeighbour(GameObject baseTile, GridData gridData)
    {

        s_neighbourList.Clear();

        var gridNode = baseTile.GetComponent<GridNodeData>();
        var currentPost = gridNode.GridPosition;

        for (var i = -1; i <= 1; i++)
        {
            if (i == 0) continue;

            updateNeighbourList(new((currentPost.x + i), currentPost.y), gridData);


            updateNeighbourList(new(currentPost.x, (currentPost.y + i)), gridData);
        }
        return s_neighbourList;

    }

    static void updateNeighbourList(Vector2Int neighPostion, GridData gridData)
    {

        var tile = gridData.GetTile(neighPostion);
        if (tile != null && tile.GetComponent<GridNodeData>().GridPosition == neighPostion)
            s_neighbourList.Add(tile);

    }
}
