using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extensions
{
    public static Vector3 ToVector3(this Vector2Int vector2Int)
    {
        return new Vector3(vector2Int.x, vector2Int.y, 0);
    }

    public static T Rand<T>(this List<T> list)
    {
        int random = UnityEngine.Random.Range(0, list.Count);
        return list[random];
    }

    public static List<T> PullWithoutReplacement<T>(this List<T> list, int numberToPull)
    {
        var rand = new System.Random();
        if (list.Count < numberToPull)
            numberToPull = list.Count;
        var returnList = list.OrderBy(t => rand.Next()).Take(numberToPull);
        return returnList.ToList();
    }
}
