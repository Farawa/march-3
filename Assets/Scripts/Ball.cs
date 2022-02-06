using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] private Image ballImage;
    private Vector2Int position;
    private bool isMoving;
    private BallColor color;
    public int Id { get; private set; }

    public void Setup(BallColor color, Vector2Int position,int id)
    {
        Id = id;
        this.color = color;
        this.position = position;
        transform.position = Field.GetCellPosition(position);
        SetImageColor(color);
    }

    private void SetImageColor(BallColor color)
    {
        Color imageColor;
        if (color == BallColor.blue) imageColor = Color.blue;
        else if (color == BallColor.green) imageColor = Color.green;
        else if (color == BallColor.red) imageColor = Color.red;
        else if (color == BallColor.violet) imageColor = Color.HSVToRGB(300, 1, 1);
        else imageColor = Color.yellow;
        ballImage.color = imageColor;
    }

    public void Reset()
    {
        StopAllCoroutines();
    }
}
