using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static Ball firstBall;
    private static Ball secondBall;

    public static void SwitchBalls(Ball ball, Vector2Int secondPosition)
    {
        var otherBall = BallsController.instance.GetBall(secondPosition);
        ball.FreeMoveTo(secondPosition);
        otherBall.FreeMoveTo(ball.position);
        ball.onEndMove += CheckResult;
        firstBall = ball;
        secondBall = otherBall;
    }

    private static void CheckResult()
    {
        var firstPos = firstBall.position;
        var secondPos = secondBall.position;
        BallsController.instance.SetBallPosition(firstBall, secondPos);
        BallsController.instance.SetBallPosition(secondBall, firstPos);
        var firstCombo = CombinationsController.SearchCombinations(firstPos, secondBall.BallColor);
        var secondCombo = CombinationsController.SearchCombinations(secondPos, firstBall.BallColor);
        if (!firstCombo && !secondCombo)
        {
            firstBall.FreeMoveTo(firstPos);
            secondBall.FreeMoveTo(secondPos);
            BallsController.instance.SetBallPosition(firstBall, firstPos);
            BallsController.instance.SetBallPosition(secondBall, secondPos);
            firstBall.isTouched = false;
            secondBall.isTouched = false;
        }
    }
}
