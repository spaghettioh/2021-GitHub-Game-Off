using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class DropArea : MonoBehaviour
{
    [SerializeField] private MouseEventChannelSO _mouseEventChannel;
    [SerializeField] private FinishEventChannelSO _finishEventChannel;

    //[Header("When a GameObject is dropped...")]
    //public UnityAction<Draggable> OnDrop;

    private Draggable _overhead;

    private void Update()
    {
        if (_overhead != null)
        {
            if (!_overhead.IsDragging)
            {
                _overhead.transform.position = transform.position;

                _mouseEventChannel.RaiseDrop(_overhead);
                _finishEventChannel.Raise(_overhead.gameObject);
                Debug.Log(_overhead.gameObject);
                _overhead = null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Draggable draggable = collision.GetComponent<Draggable>();
        if (draggable != null)
        {
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
