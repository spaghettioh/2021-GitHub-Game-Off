using UnityEngine;

[CreateAssetMenu (menuName = "Scriptable Objects/Mouse Cursor State",
    fileName = "MouseCursorState")]
public class MouseCursorStateSO : ScriptableObject
{
    public CursorStyle CursorState;
}

public enum CursorStyle
{
    Normal,
    Open,
    Grab,
    Pinch,
    Ready,
    Press,
}