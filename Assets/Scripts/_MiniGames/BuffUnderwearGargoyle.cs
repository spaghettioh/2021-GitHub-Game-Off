using UnityEngine;

public class BuffUnderwearGargoyle : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _forearmBody;
    [SerializeField] private FinishEventChannelSO _finishEventChannel;
    [SerializeField] private MouseCursorStateSO _mouseCursorState;

    private int _lifts = 0;
    private bool _isDragging;
    private bool _mouseDown;

    private void OnMouseOver()
    {
        if (MouseManager.MouseIsFree)
        {
            if (!_mouseDown)
            {
                _mouseCursorState.CursorState = CursorStyle.Ready;
            }
        }
    }

    private void OnMouseUp()
    {
        _isDragging = false;
        _mouseCursorState.CursorState = CursorStyle.Open;
    }

    private void OnMouseExit()
    {
        if (!_isDragging)
        {
            _mouseCursorState.CursorState = CursorStyle.Normal;
        }
    }

    private void OnMouseDrag()
    {
        _isDragging = true;

        // Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(_forearmBody.transform.position);

        // Get the Screen position of the mouse
        Vector2 mouseOnScreen = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        // Add the collider offset
        //Vector2 colliderOffset = GetComponent<BoxCollider2D>().offset;
        //Vector2 result = colliderOffset - positionOnScreen;

        // Get the angle between the points
        //float angle = AngleBetweenTwoPoints(result, mouseOnScreen);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        _forearmBody.MoveRotation(angle);

        float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }
    }

    private void Update()
    {
        if (!MiniGameFinish.MiniGameIsFinished)
        {
            if (_lifts >= 2)
            {
                _finishEventChannel.Raise(gameObject);
            }
        }
        else
        {
            _forearmBody.freezeRotation = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _lifts += 1;
    }
}
