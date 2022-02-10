using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Combinations
{
    public static void TryGetRow3(Vector2Int position, ref List<Vector2Int> list, BallColor ballColor)
    {
        //       20
        //       21
        // 02 12 22 32 42
        //       23
        //       24
        var ball02 = BallsController.instance.GetBall(Direction.GetPosition(position, -2, 0));
        var ball12 = BallsController.instance.GetBall(Direction.GetPosition(position, -1, 0));
        var ball32 = BallsController.instance.GetBall(Direction.GetPosition(position, 1, 0));
        var ball42 = BallsController.instance.GetBall(Direction.GetPosition(position, 2, 0));
        var ball20 = BallsController.instance.GetBall(Direction.GetPosition(position, 0, 2));
        var ball21 = BallsController.instance.GetBall(Direction.GetPosition(position, 0, 1));
        var ball23 = BallsController.instance.GetBall(Direction.GetPosition(position, 0, -1));
        var ball24 = BallsController.instance.GetBall(Direction.GetPosition(position, 0, -2));

        if (ball20)
            if (ball20.position.y == 0)
                ball20 = null;
        if (ball21)
            if (ball21.position.y == 0)
                ball21 = null;

        CheckCorrectBall(ref ball02, ballColor);
        CheckCorrectBall(ref ball12, ballColor);
        CheckCorrectBall(ref ball32, ballColor);
        CheckCorrectBall(ref ball42, ballColor);
        CheckCorrectBall(ref ball20, ballColor);
        CheckCorrectBall(ref ball21, ballColor);
        CheckCorrectBall(ref ball23, ballColor);
        CheckCorrectBall(ref ball24, ballColor);

        if (ball02 && ball12) AddBalls(ref list, new Vector2Int[] { ball02.position, ball12.position, position });//02 12 22
        if (ball12 && ball32) AddBalls(ref list, new Vector2Int[] { ball12.position, position, ball32.position });//12 22 32
        if (ball32 && ball42) AddBalls(ref list, new Vector2Int[] { position, ball42.position, ball32.position });//22 32 42
        if (ball20 && ball21) AddBalls(ref list, new Vector2Int[] { ball20.position, ball21.position, position });//20 21 22 
        if (ball21 && ball23) AddBalls(ref list, new Vector2Int[] { ball21.position, position, ball23.position });//21 22 23
        if (ball23 && ball24) AddBalls(ref list, new Vector2Int[] { position, ball23.position, ball24.position });//22 23 24
    }

    private static void CheckCorrectBall(ref Ball ball, BallColor targetColor)
    {
        if (ball)
            if (ball.BallColor != targetColor)
                ball = null;
    }

    private static void AddBalls(ref List<Vector2Int> list, Vector2Int[] positions)
    {
        for (int i = 0; i < positions.Length; i++)
            list.Add(positions[i]);
    }
}
