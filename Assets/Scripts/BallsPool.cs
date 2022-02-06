using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsPool : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    private Queue<Ball> balls = new Queue<Ball>();

    public Ball GetBall()
    {
        if (balls.Count == 0)
            CreateBall();
        var ball = balls.Dequeue();
        ball.gameObject.SetActive(true);
        return ball;
    }

    private void CreateBall()
    {
        var ball = Instantiate(ballPrefab, transform);
        ball.SetActive(false);
        balls.Enqueue(ball.GetComponent<Ball>());
    }

    private void AddBall(Ball ball)
    {
        ball.Reset();
        balls.Enqueue(ball);
        ball.gameObject.SetActive(false);
    }
}
