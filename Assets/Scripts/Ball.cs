using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ball : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler,IPointerUpHandler
{
    [SerializeField] private Image ballImage;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private TextMeshProUGUI text;
    private BallColor color;
    private bool isMoving = false;
    public bool isTouched = false;
    private Vector3 startTouchPosition;
    private float maxMouseMagnitude = 10;
    public Action onEndMove;

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

    public void MoveTo(Vector2Int position)
    {
        //StopAllCoroutines();
        var oldPosition = this.position;
        this.position = position;
        BallsController.instance.SetNewBallPotion(this, oldPosition, position);
        StartCoroutine(SendMoveWithDelay(oldPosition));
        text.text = this.position.ToString();
        isMoving = true;
        StartCoroutine(Move(position,true));
    }

    public void FreeMoveTo(Vector2Int position)
    {
        StartCoroutine(Move(position, false));
    }

    private IEnumerator Move(Vector2Int targetIndex, bool isNeedMoveAfter)
    {
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
        if (isNeedMoveAfter)
            if (!Moving.TryMoveBall(this))
                isMoving = false;
        onEndMove?.Invoke();
        onEndMove = null;
    }

    private IEnumerator SendMoveWithDelay(Vector2Int position)
    {
        yield return new WaitForSeconds(0.05f);
        Moving.MoveOthersBall(position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isMoving) return;
        startTouchPosition = Input.mousePosition;
        isTouched = true;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (isMoving||!isTouched) return;
        var mousePos = Input.mousePosition;
        if (mousePos.x - startTouchPosition.x > maxMouseMagnitude) PlayerController.SwitchBalls(this,Direction.GetRight(position));//right
        if (mousePos.x - startTouchPosition.x < -maxMouseMagnitude) PlayerController.SwitchBalls(this, Direction.GetLeft(position));//left
        if (mousePos.y - startTouchPosition.y > maxMouseMagnitude) PlayerController.SwitchBalls(this, Direction.GetTop(position));//top
        if (mousePos.y - startTouchPosition.y < -maxMouseMagnitude) PlayerController.SwitchBalls(this, Direction.GetBottom(position));//bottom
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouched = false;
    }
}
