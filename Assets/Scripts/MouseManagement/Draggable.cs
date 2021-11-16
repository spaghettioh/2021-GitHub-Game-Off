using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Collider2D))]
public class Draggable : MonoBehaviour
{
    [SerializeField] private MouseCursorStateSO _mouseCursorState;
    [SerializeField] private ClickEventChannelSO _clickEventChannel;

    //private Vector3 _offset;
    private bool _isDragging;
    public bool IsDragging
    {
        get { return _isDragging; }
        //private set { _isDragging = value; }
    }

    private void OnMouseOver()
    {
        if (IsMouseFree())
        {
            if (!_isDragging)
            {
                _mouseCursorState.CursorState = CursorStyle.Open;
            }
        }
    }

    private void OnMouseDown()
    {
        //_offset = transform.position - Camera.main.ScreenToWorldPoint(
            //new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        // Snap the object to the mouse cursor
        transform.position = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        _mouseCursorState.CursorState = CursorStyle.Grab;
        transform.localScale = new Vector3(1.25f, 1.25f, 1f);
        _isDragging = true;
    }

    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, 0);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        //Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint)
        //    + _offset;
        transform.position = new Vector3(curPosition.x, curPosition.y, 0);
    }

    private void OnMouseUp()
    {
        _mouseCursorState.CursorState = CursorStyle.Open;
        _isDragging = false;
        transform.localScale = new Vector3(1f, 1f, 1f);
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
