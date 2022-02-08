using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Combinations
{
    public static bool TryGetRow3(Vector2Int position, ref List<Vector2Int> list, BallColor ballColor)
    {
        bool isCombinationFinded = false;
        var leftPos = Direction.GetLeft(position);
        var rightPos = Direction.GetRight(position);
        var topPos = Direction.GetTop(position);
        var bottomPos = Direction.GetBottom(position);

        var leftBall = BallsPool.TryGetBallFromAll(leftPos);
        var rightBall = BallsPool.TryGetBallFromAll(rightPos);
        var topBall = BallsPool.TryGetBallFromAll(topPos);
        var bottomBall = BallsPool.TryGetBallFromAll(bottomPos);


        if (leftBall && rightBall)
            if (leftBall.BallColor == ballColor && leftBall.BallColor == ballColor && !leftBall.IsMoving && !rightBall.IsMoving)
            {
                list.Add(leftPos);
                list.Add(rightPos);
                isCombinationFinded = true;
            }
        if (topBall && bottomBall)
            if (topBall.BallColor == ballColor && bottomBall.BallColor == ballColor && !topBall.IsMoving && !bottomBall.IsMoving)
            {
                list.Add(topPos);
                list.Add(bottomPos);
                isCombinationFinded = true;
            }
        return isCombinationFinded;
        //   *
        // * * *
        //   *
    }
}
