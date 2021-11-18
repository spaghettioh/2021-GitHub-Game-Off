using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOlogistArms : MonoBehaviour
{
    [SerializeField] private int _rotationSpeed;
    [Space]
    [SerializeField] private MouseCursorStateSO _mouseCursorState;
    private Rigidbody2D _rigidBody;
    private float _rotationZ;
    private bool _isDragging;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 mouseScreenPoint =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition =
            new Vector3(mouseScreenPoint.x, mouseScreenPoint.y, 0);
        Vector3 armDelta = mousePosition - transform.position;

        _rotationZ = Mathf.Atan2(armDelta.x, -armDelta.y) * Mathf.Rad2Deg;
    }

    private void OnMouseDrag()
    {
        _rigidBody.MoveRotation(Mathf.LerpAngle(_rigidBody.rotation,
            _rotationZ, _rotationSpeed * Time.deltaTime));
    }

    private void OnMouseOver()
    {
        if (MouseManager.MouseIsFree)
        {
            if (!_isDragging)
            {
                _mouseCursorState.CursorState = CursorStyle.Open;
            }
        }
    }

    private void OnMouseDown()
    {
        _isDragging = true;
        _mouseCursorState.CursorState = CursorStyle.Grab;

        // Snap the object to the mouse cursor
        transform.position = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        transform.localScale = new Vector3(1.25f, 1.25f, 1f);
    }

    private void OnMouseUp()
    {
        _isDragging = false;
        transform.localScale = new Vector3(1f, 1f, 1f);
        _mouseCursorState.CursorState = CursorStyle.Open;
    }

    private void OnMouseExit()
    {
        if (!_isDragging)
        {
            _mouseCursorState.CursorState = CursorStyle.Normal;
        }
    }
}
