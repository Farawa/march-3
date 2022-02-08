using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private CellType cellType;
    private Vector2Int cellIndex;
    public int captureBallId { get; private set; } = -1;

    public void Capture(int id)
    {
        captureBallId = id;
    }

    public void Clear()
    {
        captureBallId = -1;
        if (cellType == CellType.spawner)
            Spawn();
    }

    public void Setup(CellType cellType, Vector2Int index)
    {
        this.cellType = cellType;
        cellIndex = index;
        if (cellType == CellType.spawner)
            Field.GenerateDone += Spawn;
    }

    public void Spawn()
    {
        var ball = BallsPool.GetBall();
        var colorsCount = Enum.GetNames(typeof(BallColor)).Length;
        var color = UnityEngine.Random.Range(0, colorsCount);
        ball.Setup((BallColor)color, cellIndex);
        captureBallId = ball.Id;
        //TODO move new ball;
        Moving.TryMoveBall(cellIndex);
    }
}