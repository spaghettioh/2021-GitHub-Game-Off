using UnityEngine;
using UnityEngine.Events;

public class Draggable : MonoBehaviour
{
    [SerializeField] private MouseCursorStateSO _mouseCursorState;
    [SerializeField] private MouseEventChannelSO _mouseEventChannel;
    [SerializeField] private UnityEvent _onGrab;
    [SerializeField] private UnityEvent _onDrop;

    private bool _isDragging;
    public bool IsDragging
    {
        get { return _isDragging; }
    }

    private void OnMouseOver()
    {
        if (!MiniGameFinish.InteractionsDisabled)
        {
            if (IsMouseFree())
            {
                if (!_isDragging)
                {
                    _mouseCursorState.CursorState = CursorStyle.Open;
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (!MiniGameFinish.InteractionsDisabled)
        {
            _isDragging = true;
            _mouseCursorState.CursorState = CursorStyle.Grab;

            // Snap the object to the mouse cursor
            transform.position = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            transform.localScale = new Vector3(1.25f, 1.25f, 1f);
            _onGrab.Invoke();
        }
    }

    private void OnMouseDrag()
    {
        if (!MiniGameFinish.InteractionsDisabled)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, 0);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            transform.position = new Vector3(curPosition.x, curPosition.y, 0);
        }
    }

    private void OnMouseUp()
    {
        if (!MiniGameFinish.InteractionsDisabled)
        {
            if (_isDragging)
            {
                _onDrop.Invoke();
            }
            _isDragging = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
            _mouseCursorState.CursorState = CursorStyle.Normal;
        }
    }

    private void OnMouseExit()
    {
        if (!_isDragging)
        {
            _mouseCursorState.CursorState = CursorStyle.Normal;
        }
    }

    private bool IsMouseFree()
    {
        if (_mouseCursorState.CursorState == CursorStyle.Normal)
        {
            return true;
        }

        return false;
    }
}
