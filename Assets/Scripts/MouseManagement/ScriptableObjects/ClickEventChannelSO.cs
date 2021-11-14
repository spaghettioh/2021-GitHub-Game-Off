using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[CreateAssetMenu (menuName = "Scriptable Objects/Click Event Channel",
    fileName = "ClickEventChannel")]
public class ClickEventChannelSO : ScriptableObject
{
    public UnityAction<Vector2> OnClick;

    public void Clicked(Vector2 mousePosition)
    {
        OnClick.Invoke(mousePosition);
    }
}
