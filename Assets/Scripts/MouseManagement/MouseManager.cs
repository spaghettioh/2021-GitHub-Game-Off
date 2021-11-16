using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [Header("Cursors")]
    [SerializeField] private Texture2D _normalState;
    [SerializeField] private Texture2D _openState;
    [SerializeField] private Texture2D _grabState;
    [SerializeField] private Texture2D _readyState;
    [SerializeField] private Texture2D _pressState;

    [Header("Scriptable objects")]
    [SerializeField] private MouseCursorStateSO _mouseCursorState;
    [SerializeField] private MouseEventChannelSO _mouseEventChannel;

    private CursorStyle currentCursorStyle;

    private CursorMode _cursorMode = CursorMode.Auto;
    private Vector2 _hotSpot = new Vector2(8, 0);

    private void OnEnable()
    {
        _mouseEventChannel.OnClick += MouseClicked;
    }

    private void Start()
    {
        Cursor.SetCursor(_normalState, _hotSpot, _cursorMode);
    }

    private void Update()
    {
        if (currentCursorStyle != _mouseCursorState.CursorState)
        {
            if (_mouseCursorState.CursorState == CursorStyle.Normal)
            {
                Cursor.SetCursor(_normalState, _hotSpot, _cursorMode);
            }
            else if (_mouseCursorState.CursorState == CursorStyle.Open)
            {
                Cursor.SetCursor(_openState, _hotSpot, _cursorMode);
            }
            else if (_mouseCursorState.CursorState == CursorStyle.Grab)
            {
                Cursor.SetCursor(_grabState, _hotSpot, _cursorMode);
            }
            else if (_mouseCursorState.CursorState == CursorStyle.Ready)
            {
                Cursor.SetCursor(_readyState, _hotSpot, _cursorMode);
            }
            else if (_mouseCursorState.CursorState == CursorStyle.Press)
            {
                Cursor.SetCursor(_pressState, _hotSpot, _cursorMode);
            }

            currentCursorStyle = _mouseCursorState.CursorState;
        }
    }

    private void MouseClicked(Vector2 position)
    {
        Debug.Log("Mouse clicked over object!");
    }
}
