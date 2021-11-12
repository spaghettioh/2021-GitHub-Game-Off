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
    [SerializeField] private ClickEventChannelSO _clickEventChannel;

    private CursorStyle currentCursorStyle;

    private CursorMode _cursorMode = CursorMode.Auto;
    private Vector2 _hotSpot = new Vector2();

    private void OnEnable()
    {
        _clickEventChannel.OnClick += MouseClicked;
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
                _hotSpot = new Vector2(17, 18);
                Cursor.SetCursor(_normalState, _hotSpot, _cursorMode);
            }
            else if (_mouseCursorState.CursorState == CursorStyle.Open)
            {
                _hotSpot = new Vector2(27, 29);
                Cursor.SetCursor(_openState, _hotSpot, _cursorMode);
            }
            else if (_mouseCursorState.CursorState == CursorStyle.Grab)
            {
                _hotSpot = new Vector2(19, 14);
                Cursor.SetCursor(_grabState, _hotSpot, _cursorMode);
            }
            else if (_mouseCursorState.CursorState == CursorStyle.Ready)
            {
                _hotSpot = new Vector2(12, 11);
                Cursor.SetCursor(_readyState, _hotSpot, _cursorMode);
            }
            else if (_mouseCursorState.CursorState == CursorStyle.Press)
            {
                _hotSpot = new Vector2(5, 47);
                Cursor.SetCursor(_pressState, _hotSpot, _cursorMode);
            }

            currentCursorStyle = _mouseCursorState.CursorState;
        }
    }

    private Vector2 GetHotSpot(Texture2D cursor)
    {
        return new Vector2(0, 0);
    }

    private void MouseClicked(Vector2 position)
    {
        Debug.Log("Mouse clicked over object!");
    }
}
