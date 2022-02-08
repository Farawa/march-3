using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsPool : MonoBehaviour
{
    private static BallsPool instance = null;
    [SerializeField] private GameObject ballPrefab;
    private static Dictionary<int, Ball> allBalls = new Dictionary<int, Ball>();
    private static Queue<Ball> balls = new Queue<Ball>();
    private int currentBallId = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else throw new System.Exception("BallsPool is not single");
    }

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        balls = new Queue<Ball>();
    }

    public static Ball GetBall()
    {//TODO queue
        instance.CreateBall();
        var ball = balls.Dequeue();
        ball.gameObject.SetActive(true);
        return ball;
    }

    public static Ball GetBallFromAll(int index)
    {
        Ball ball;
        if (allBalls.TryGetValue(index, out ball))
            return ball;
        else
            return null;
    }

    public static Ball TryGetBallFromAll(Vector2Int position)
    {
        foreach (var ball in allBalls)
        {
            if (ball.Value.Position == position)
            {
                return ball.Value;
            }
        }
        return null;
    }

    public static BallColor TryGetBallColor(Vector2Int position)
    {
        var ball = TryGetBallFromAll(position);
        if(ball)
        {
            return ball.BallColor;
        }
        else
        {
            return BallColor.non;
        }
    }

    private void CreateBall()
    {
        var ball = Instantiate(ballPrefab, transform).GetComponent<Ball>();
        ball.gameObject.SetActive(false);
        balls.Enqueue(ball);
        allBalls.Add(currentBallId, ball);
        ball.SetId(currentBallId);
        currentBallId++;
        print("ball created");
    }

    public static void AddBall(Ball ball)
    {
        ball.Reset();
        balls.Enqueue(ball);
        ball.gameObject.SetActive(false);
    }
    public static void AddBall(Vector2Int position)
    {
        var ball = TryGetBallFromAll(position);
        if (!ball) throw new System.Exception();
        ball.Reset();
        balls.Enqueue(ball);
        ball.gameObject.SetActive(false);
    }
}
