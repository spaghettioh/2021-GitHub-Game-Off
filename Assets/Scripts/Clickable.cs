using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Clickable : MonoBehaviour
{
    [SerializeField] private MouseCursorStateSO _mouseCursorState;
    [SerializeField] private ClickEventChannelSO _clickEventChannel;

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
        _clickEventChannel.Clicked(_offset);
        _mouseCursorState.CursorState = CursorStyle.Press;
        _mouseDown = true;
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
