using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Moving
{
    public static void MoveOthersBall(Vector2Int position)
    {
        //TODO sides
        var topPos = Direction.GetTop(position);
        var topBall = BallsController.instance.GetBall(topPos);
        if (topBall)
        {
            topBall.MoveTo(position);
        }
    }

    public static void MoveOthersBall(ref List<Vector2Int> positions)
    {
        foreach (var pos in positions)
        {
            MoveOthersBall(pos);
        }
    }

    public static void TryMoveBall(Vector2Int index)
    {
        var ball = BallsController.instance.GetBall(index);
        if (ball)
            TryMoveBall(ball);
    }

    public static bool TryMoveBall(Ball ball)
    {

        //Vector2Int movePosition;//TODO sides
        var bot = Direction.GetBottom(ball.position);
        if (Field.IsCellFree(bot))
        {
            var ballPos = ball.position;
            ball.MoveTo(bot);
            if (ballPos.y == 0)
            {
                Field.instance.SpawnOnCell(ballPos);
            }
            return true;
        }
        else
        {
            var bottomBall = BallsController.instance.GetBall(bot);
            if (bottomBall)
                if (!bottomBall.IsMoving)
                    CombinationsController.SearchCombinations(ball.position, ball.BallColor);
            return false;
        }
    }
}
