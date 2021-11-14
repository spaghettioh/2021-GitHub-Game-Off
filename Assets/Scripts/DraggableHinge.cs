using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (DistanceJoint2D))]
public class DraggableHinge : MonoBehaviour
{
    [SerializeField] private MouseCursorStateSO _mouseCursorState;
    [SerializeField] private ClickEventChannelSO _clickEventChannel;

    private Vector3 _screenPoint;
    private Vector3 _offset;
    private bool _mouseDown;

    private DistanceJoint2D jointComponent;

    private void Start()
    {
        jointComponent = GetComponent<DistanceJoint2D>();
    }

    private void OnMouseOver()
    {
        if (!_mouseDown)
        {
            _mouseCursorState.CursorState = CursorStyle.Open;
        }
    }

    private void OnMouseDown()
    {
        _offset = transform.position - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                _screenPoint.z));
        _mouseCursorState.CursorState = CursorStyle.Grab;
        _mouseDown = true;
    }

    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, _screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint)
            + _offset;
        if (Vector3.Distance(curPosition, transform.position) <= jointComponent.distance)
        {
            transform.position = curPosition;
        }
        //transform.Translate(curPosition);
    }

    private void OnMouseUp()
    {
        _mouseCursorState.CursorState = CursorStyle.Open;
        _mouseDown = false;
    }

    private void OnMouseExit()
    {
        if (!_mouseDown)
        {
            _mouseCursorState.CursorState = CursorStyle.Normal;
        }
    }
}
