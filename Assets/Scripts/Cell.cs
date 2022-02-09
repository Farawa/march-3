using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private CellType cellType;
    private Vector2Int cellIndex;

    public void Setup(CellType cellType, Vector2Int index)
    {
        this.cellType = cellType;
        cellIndex = index;
        if (cellType == CellType.spawner)
            Field.GenerateDone += Spawn;
    }

    public void Spawn()
    {
        var ball = BallsController.instance.SpawnBall();
        var colorsCount = Enum.GetNames(typeof(BallColor)).Length;
        var color = UnityEngine.Random.Range(0, colorsCount);
        ball.Setup((BallColor)color, cellIndex);
        Moving.TryMoveBall(cellIndex);
    }
}