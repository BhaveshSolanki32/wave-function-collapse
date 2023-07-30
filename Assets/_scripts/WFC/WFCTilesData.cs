using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TilesDataScriptableObject", menuName = "ScriptableObject/Create TilesData")]
public class WFCTilesData : ScriptableObject
{
    //SOCKET naming convention made 3 value each containing initials for the 3 colors on the top, center and bottom of the tile.
    //For  up and down facing side we go from left to right.
    //each tile key has vlaue of string list containg socket codes in clockwise order (top, right, bottom, left)

    public Dictionary<string, List<string>> ValidTilesDictionary { get; private set; } = new Dictionary<string, List<string>>()
    {
        { "water", new List<string>() {"GB-GB-GB", "GB-GB-GB", "GB-GB-GB", "GB-GB-GB" } },
        { "voidRock",new List<string>() {"B-B-B", "B-B-B", "B-B-B","B-B-B" } },
        { "sU", new List<string>() {"G-G-G","G-B-B","B-B-B","G-B-B" } },
        { "sR", new List<string>() {"B-B-G", "G-G-G", "B-B-G","B-B-B" } },
        { "sL", new List<string>() {"G-B-B","B-B-B","G-B-B","G-G-G" } },
        { "sB", new List<string>() {"B-B-B", "B-B-G", "G-G-G","B-B-G" } },
        { "sUR", new List<string>() {"G-G-G", "G-G-G", "B-B-G","G-B-B" } },
        { "sUL", new List<string>() {"G-G-G", "G-B-B", "G-B-B","G-G-G" } },
        { "sDR", new List<string>() {"B-B-G", "G-G-G", "G-G-G","B-B-G" } },
        { "sDL", new List<string>() {"G-B-B", "B-B-G", "G-G-G", "G-G-G" } },
        {"URCorner", new List<string>() {"B-B-G","G-B-B","B-B-B","B-B-B" } },
        {"ULCorner", new List<string>() { "G-B-B", "B-B-B", "B-B-B", "G-B-B" } },
        {"sU grass", new List<string>(){ "G-G-G", "G-B-B", "B-B-B", "G-B-B" } },
        {"sU grass2", new List<string>(){ "G-G-G", "G-B-B", "B-B-B", "G-B-B" } },
        {"sU grass3", new List<string>(){ "G-G-G", "G-B-B", "B-B-B", "G-B-B" } },
        { "fish 2", new List<string>() {"GB-GB-GB", "GB-GB-GB", "GB-GB-GB", "GB-GB-GB" } },
        { "sUL grass", new List<string>() {"G-G-G", "G-B-B", "G-B-B","G-G-G" } },
        { "sUR grass", new List<string>() {"G-G-G", "G-G-G", "B-B-G","G-B-B" } },


    };
    //exception only GB goes with G and g doesn't goes wth g
}
