using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombinationsController 
{
    public static bool SearchCombinations(Vector2Int position,BallColor ballColor)
    {
        var list = new List<Vector2Int>();
        bool isCombinationFounded = false;
        if (Combinations.TryGetRow3(position, ref list, ballColor))
            isCombinationFounded = true;
        //TODO other combinations
        Blow(ref list);
        Moving.MoveOthersBall(ref list);
        return isCombinationFounded;
    }

    private static void Blow(ref List<Vector2Int> positions)
    {
        Debug.Log("blow");
        foreach(var pos in positions)
        {
            BallsPool.AddBall(pos);
            //TODO add points
        }
    }
}
