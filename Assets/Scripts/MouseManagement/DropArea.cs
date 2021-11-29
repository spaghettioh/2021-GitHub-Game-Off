using UnityEngine;

public class DropArea : MonoBehaviour
{
    [SerializeField] private MouseEventChannelSO _mouseEventChannel;
    [SerializeField] private FinishEventChannelSO _finishEventChannel;

    private Draggable _overhead;

    private void Update()
    {
        if (_overhead != null)
        {
            if (!_overhead.IsDragging)
            {
                _overhead.transform.position = transform.position;

                _mouseEventChannel.RaiseDrop(_overhead);

                if (!MiniGameFinish.MiniGameIsFinished)
                {
                    _finishEventChannel.Raise(_overhead.gameObject);
                }

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
