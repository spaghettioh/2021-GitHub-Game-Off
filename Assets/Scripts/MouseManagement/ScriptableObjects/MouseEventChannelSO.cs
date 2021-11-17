using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu (menuName = "Scriptable Objects/Click Event Channel",
    fileName = "MouseEventChannel")]
public class MouseEventChannelSO : ScriptableObject
{
    public UnityAction<Vector2> OnHoverEnter;
    public void RaiseHoverEnter(Vector2 mousePosition)
    {
        if (OnHoverEnter != null)
        {
            OnHoverEnter.Invoke(mousePosition);
        }
        else
        {
            Debug.LogWarning("Mouse hover enter event raised but nothing" +
                " listens...");
        }
    }

    public UnityAction<Vector2> OnHoverExit;
    public void RaiseHoverExit(Vector2 mousePosition)
    {
        if (OnHoverExit != null)
        {
            OnHoverExit.Invoke(mousePosition);
        }
        else
        {
            Debug.LogWarning("Mouse hover exit event raised but nothing" +
                " listens...");
        }
    }

    public UnityAction<Vector2> OnClick;
    public void RaiseClick(Vector2 mousePosition)
    {
        if (OnClick != null)
        {
            OnClick.Invoke(mousePosition);
        }
        else
        {
            Debug.LogWarning("Mouse click event raised but nothing listens...");
        }
    }

    public UnityAction<Draggable> OnDrag;
    public void RaiseDrag(Draggable draggable)
    {
        if (OnDrag != null)
        {
            OnDrag.Invoke(draggable);
        }
        else
        {
            Debug.LogWarning("Mouse drag event raised but nothing listens...");
        }
    }

    public UnityAction<Draggable> OnDrop;
    public void RaiseDrop(Draggable draggable)
    {
        if (OnDrop != null)
        {
            OnDrop.Invoke(draggable);
        }
        else
        {
            Debug.LogWarning("Mouse drop event raised but nothing listens...");
        }
    }
}
