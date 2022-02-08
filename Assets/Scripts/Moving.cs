using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Moving
{
    public static async void MoveOthersBall(Vector2Int position)
    {
        //TODO sides
        var top = Direction.GetTop(position);
        //var cell = Field.GetCell(currentIndex);
        var topBall = BallsPool.TryGetBallFromAll(top);
        if (topBall)
        {
            await System.Threading.Tasks.Task.Delay(100);
            if (topBall.IsMoving) return;
            topBall.MoveTo(position);
        }
    }

    public static void MoveOthersBall(ref List<Vector2Int> positions)
    {
        foreach(var pos in positions)
        {
            MoveOthersBall(pos);
        }
    }

    public static void TryMoveBall(Vector2Int index)
    {
        var ball = BallsPool.TryGetBallFromAll(index);
        TryMoveBall(ball);
    }

    public static bool TryMoveBall(Ball ball)
    {
        //Vector2Int moveIndex;//TODO sides
        var bot = Direction.GetBottom(ball.Position);
        if (!ball) throw new System.Exception();
        if (Field.IsCellFree(bot))
        {
            ball.MoveTo(bot);
            return true;
        }
        return false;
    }
}
