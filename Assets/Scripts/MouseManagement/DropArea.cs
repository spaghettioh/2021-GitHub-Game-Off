using UnityEngine;
using UnityEngine.Events;

public class DropArea : MonoBehaviour
{
    [SerializeField] private MouseEventChannelSO _mouseEventChannel;
    [SerializeField] private FinishEventChannelSO _finishEventChannel;
    [SerializeField] private UnityEvent _onDrop;

    private bool _canDrop = true;
    private Draggable _overhead;

    private void Update()
    {
        if (_overhead != null && _canDrop)
        {
            if (!_overhead.IsDragging)
            {
                _overhead.transform.position = transform.position;

                _mouseEventChannel.RaiseDrop(_overhead);

                if (!MiniGameFinish.InteractionsDisabled)
                {
                    _finishEventChannel.Raise(_overhead.gameObject);
                }

                _onDrop.Invoke();
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

    public void DisableDropArea()
    {
        _canDrop = false;
    }
}
