using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsController : MonoBehaviour
{
    public static BallsController instance = null;
    [SerializeField] private GameObject ballPrefab;
    private Dictionary<Vector2Int, Ball> places = new Dictionary<Vector2Int, Ball>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            throw new System.Exception("this is not single");
        CreateGrid();
    }

    private void CreateGrid()
    {
        var fieldSize = Field.instance.FieldSize;
        for (int x = 0; x < fieldSize.x; x++)
        {
            for (int y = 0; y < fieldSize.y; y++)
            {
                places.Add(new Vector2Int(x, y), null);
            }
        }
    }

    public void SetNewBallPotion(Ball ball, Vector2Int oldPosition, Vector2Int newPosition)
    {
        places[oldPosition] = null;
        places[newPosition] = ball;
    }

    public Ball GetBall(Vector2Int position)
    {
        Ball ball;
        if (places.TryGetValue(position, out ball))
            return ball;
        return null;
    }

    public BallColor TryGetBallColor(Vector2Int position)
    {
        var ball = GetBall(position);
        if (ball)
            return ball.BallColor;
        else
            return BallColor.non;
    }

    public void ClearPosition(Vector2Int position)
    {
        places[position] = null;
    }

    public void DestroyBall(Vector2Int position)
    {
        var ball = places[position];
        places[position] = null;
        Destroy(ball.gameObject);
    }

    public void SetBallPosition(Ball ball,Vector2Int position)
    {
        places[position] = ball;
    }

    public Ball SpawnBall()
    {
        return Instantiate(ballPrefab, transform).GetComponent<Ball>();
    }
}
