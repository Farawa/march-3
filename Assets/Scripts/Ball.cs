using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] private Image ballImage;
    [SerializeField] private float moveSpeed = 1;
    private Vector2Int position;
    private bool isMoving = false;
    private BallColor color;
    private int id;
    public Vector2Int Position { get => position; }
    public int Id { get => id; }
    public BallColor BallColor { get => color; }
    public bool IsMoving { get => isMoving; }

    public void Setup(BallColor color, Vector2Int position)
    {
        this.color = color;
        this.position = position;
        transform.position = Field.GetCellPosition(position).Value;
        SetImageColor(color);
    }

    public void SetId(int id)
    {
        this.id = id;
    }

    private void SetImageColor(BallColor color)
    {
        Color imageColor;
        if (color == BallColor.blue) imageColor = Color.blue;
        else if (color == BallColor.green) imageColor = Color.green;
        else if (color == BallColor.red) imageColor = Color.red;
        else if (color == BallColor.violet) imageColor = new Color(1, 0, 1);
        else imageColor = Color.yellow;
        ballImage.color = imageColor;
    }

    public void MoveTo(Vector2Int index)
    {
        isMoving = true;
        StartCoroutine(Move(index));
    }

    private IEnumerator Move(Vector2Int targetIndex)
    {
        Field.CaptureCell(targetIndex, id);
        Field.ClearCell(position);
        Moving.MoveOthersBall(position);
        position = targetIndex;
        var startPosition = transform.position;
        var targetPosition = Field.GetCellPosition(targetIndex).Value;
        var totalMagnitude = (targetPosition - startPosition).magnitude;
        var direction = (targetPosition - startPosition).normalized;
        var distansTraveled = 0f;
        while (true)
        {
            var moveVector = direction * Time.deltaTime * moveSpeed;
            transform.position += moveVector;
            distansTraveled += moveVector.magnitude;
            yield return null;
            if (distansTraveled >= totalMagnitude)
            {
                transform.position = targetPosition;
                break;
            }
        }
        if (CombinationsController.SearchCombinations(position, color))
            yield break;
        if (Moving.TryMoveBall(this))
            isMoving = false;
    }

    public void Reset()
    {
        StopAllCoroutines();
        isMoving = false;
        position = -Vector2Int.one;
    }
}
