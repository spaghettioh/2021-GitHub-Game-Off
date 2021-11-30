using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Clickable : MonoBehaviour
{
    [SerializeField] private MouseCursorStateSO _mouseCursorState;
    [SerializeField] private MouseEventChannelSO _clickEventChannel;

    [SerializeField] private UnityEvent _onClick;

    private Vector3 _screenPoint;
    private Vector3 _offset;
    private bool _mouseDown;

    private void OnMouseOver()
    {
        if (!_mouseDown)
        {
            _mouseCursorState.CursorState = CursorStyle.Ready;
        }
    }

    private void OnMouseDown()
    {
        _offset = transform.position - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                _screenPoint.z)
            );
        _clickEventChannel.RaiseClick(_offset);
        _mouseCursorState.CursorState = CursorStyle.Press;
        _mouseDown = true;
        _onClick.Invoke();
    }

    private void OnMouseUp()
    {
        _mouseCursorState.CursorState = CursorStyle.Ready;
        _mouseDown = false;
    }

    private void OnMouseExit()
    {
        _mouseCursorState.CursorState = CursorStyle.Normal;
    }
}
