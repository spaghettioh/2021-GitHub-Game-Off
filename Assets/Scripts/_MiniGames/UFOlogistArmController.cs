using UnityEngine;

public class UFOlogistArmController : MonoBehaviour
{
    [SerializeField] private int _rotationSpeed;
    [Space]
    [SerializeField] private MouseCursorStateSO _mouseCursorState;
    private Rigidbody2D _rigidBody;
    private float _rotationZ;
    private bool _isDragging;

    private void Awake()
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
