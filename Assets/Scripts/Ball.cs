using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] private Image ballImage;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private TextMeshProUGUI text;
    private BallColor color;
    private bool isMoving = false;

    public Vector2Int position;
    public BallColor BallColor { get => color; }
    public bool IsMoving { get => isMoving; }

    public void Setup(BallColor color, Vector2Int position)
    {
        this.color = color;
        this.position = position;
        transform.position = Field.GetCellPosition(position).Value;
        SetImageColor(color);
        BallsController.instance.SetBallPosition(this, position);
        text.text = position.ToString();
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
        StopAllCoroutines();
        StartCoroutine(Move(index));
    }

    private IEnumerator Move(Vector2Int targetIndex)
    {
        isMoving = true;
        var oldPosition = position;
        position = targetIndex;
        BallsController.instance.SetNewBallPotion(this, oldPosition, targetIndex);
        Moving.MoveOthersBall(position);
        text.text = position.ToString();
        var startPosition = transform.position;
        var targetPosition = Field.GetCellPosition(targetIndex).Value;
        var progress = 0f;
        while (true)
        {
            progress += moveSpeed;
            transform.position = (targetPosition - startPosition) * (progress / 100) + startPosition;
            if (progress >= 100)
            {
                transform.position = targetPosition;
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        if (!Moving.TryMoveBall(this))
            isMoving = false;
    }
}
