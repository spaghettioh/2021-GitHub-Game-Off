using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class DropArea : MonoBehaviour
{
    [SerializeField] private MouseCursorStateSO _mouseCursorState;

    [Header("When a GameObject is dropped...")]
    public UnityAction _onDrop;

    private Draggable _overhead;

    private void Update()
    {
        if (_overhead != null)
        {
            if (!_overhead.IsDragging)
            {
                _overhead.transform.position = transform.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Draggable draggable = collision.GetComponent<Draggable>();
        if (draggable != null)
        {
            Debug.Log($"{name}: {collision.gameObject.name} overhead");

            _overhead = draggable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Draggable draggable = collision.GetComponent<Draggable>();
        if (draggable != null)
        {
            _overhead = null;
        }
    }
}
