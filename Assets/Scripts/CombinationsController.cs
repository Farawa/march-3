using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombinationsController
{
    public static bool SearchCombinations(Vector2Int position, BallColor ballColor)
    {
        Debug.Log("search combinations");
        var list = new List<Vector2Int>();
        bool isCombinationFounded = false;
        Combinations.TryGetRow3(position, ref list, ballColor);
        //TODO other combinations
        if (list.Count != 0)
        {
            isCombinationFounded = true;
            Blow(ref list);
        }
        Moving.MoveOthersBall(ref list);
        return isCombinationFounded;
    }

    private static void Blow(ref List<Vector2Int> positions)
    {
        var blowString = "blow ";
        foreach(var pos in positions)
        {
            blowString+=" "+pos;
        }
        Debug.Log(blowString);
        foreach (var pos in positions)
        {
            BallsController.instance.DestroyBall(pos);
            //TODO add points
        }
    }
}
