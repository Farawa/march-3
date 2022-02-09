using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Combinations
{
    public static void TryGetRow3(Vector2Int position, ref List<Vector2Int> list, BallColor ballColor)
    {
        //       30
        //       31
        // 02 12 22 32 42
        //       33
        //       34
        var ball02 = BallsController.instance.GetBall(Direction.GetPosition(position, -2, 0));
        var ball12 = BallsController.instance.GetBall(Direction.GetPosition(position, -1, 0));
        var ball32 = BallsController.instance.GetBall(Direction.GetPosition(position, 1, 0));
        var ball42 = BallsController.instance.GetBall(Direction.GetPosition(position, 2, 0));
        var ball30 = BallsController.instance.GetBall(Direction.GetPosition(position, 0, 2));
        var ball31 = BallsController.instance.GetBall(Direction.GetPosition(position, 0, 1));
        var ball33 = BallsController.instance.GetBall(Direction.GetPosition(position, 0, -1));
        var ball34 = BallsController.instance.GetBall(Direction.GetPosition(position, 0, -2));

        if (ball30)
            if (ball30.position.y == 0)
                ball30 = null;
        if (ball31)
            if (ball31.position.y == 0)
                ball31 = null;
        if (ball02)
            if (ball02.BallColor != ballColor || ball02.IsMoving)
                ball02 = null;
        if (ball12)
            if (ball12.BallColor != ballColor || ball12.IsMoving)
                ball12 = null;
        if (ball32)
            if (ball32.BallColor != ballColor || ball32.IsMoving)
                ball32 = null;
        if (ball42)
            if (ball42.BallColor != ballColor || ball42.IsMoving)
                ball42 = null;
        if (ball30)
            if (ball30.BallColor != ballColor || ball30.IsMoving)
                ball30 = null;
        if (ball31)
            if (ball31.BallColor != ballColor || ball31.IsMoving)
                ball31 = null;
        if (ball33)
            if (ball33.BallColor != ballColor || ball33.IsMoving)
                ball33 = null;
        if (ball34)
            if (ball34.BallColor != ballColor || ball34.IsMoving)
                ball34 = null;

        if (ball02 && ball12) AddBalls(ref list, new Vector2Int[] { ball02.position, ball12.position, position });
        if (ball12 && ball32) AddBalls(ref list, new Vector2Int[] { ball12.position, ball32.position, position });
        if (ball32 && ball42) AddBalls(ref list, new Vector2Int[] { ball42.position, ball32.position, position });
        if (ball30 && ball31) AddBalls(ref list, new Vector2Int[] { ball30.position, ball31.position, position });
        if (ball31 && ball33) AddBalls(ref list, new Vector2Int[] { ball31.position, ball33.position, position });
        if (ball33 && ball34) AddBalls(ref list, new Vector2Int[] { ball33.position, ball34.position, position });
    }

    private static void AddBalls(ref List<Vector2Int> list, Vector2Int[] positions)
    {
        for (int i = 0; i < positions.Length; i++)
            list.Add(positions[i]);
    }
}
