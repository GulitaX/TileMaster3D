using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Level", menuName ="Scriptable Object/Level")]
public class LevelSO : ScriptableObject
{
    public int levelIndex;
    public string levelName;

    public List<TileData> levelTileDatas;

    public int sumOfTriplets()
    {
        int sum = 0;
        foreach (var tile in levelTileDatas)
        {
            sum += tile.numberOfTriplets;
        }
        return sum;
    }

    public int sumOfTiles()
    {
        return sumOfTriplets() * 3;
    }
}

