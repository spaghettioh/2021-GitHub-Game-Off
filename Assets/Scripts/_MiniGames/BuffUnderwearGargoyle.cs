using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUnderwearGargoyle : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _forearm;
    [SerializeField] private FinishEventChannelSO _finishEventChannel;

    private float _liftThreshold = 430f;
    private float _restThreshold = 630f;

    private int _lifts = 0;

    [SerializeField] private int _rotationSpeed;
    [Space]

    private float _rotationZ;
    private bool _isDragging;

    // Forces the arm to fall to resting position
    private bool _canLift = true;

    private enum State
    {
        Lifting,
        Falling,
    }
    private State state = State.Lifting;

    private void OnMouseDrag()
    {
        _forearm.MoveRotation(Mathf.LerpAngle(_forearm.rotation,
            _rotationZ, _rotationSpeed * Time.deltaTime));
    }

    private void Update()
    {
        Vector2 mouseScreenPoint =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition =
            new Vector3(mouseScreenPoint.x, mouseScreenPoint.y, 0);
        Vector3 armDelta = mousePosition - _forearm.transform.position;

        _rotationZ = Mathf.Atan2(armDelta.x, -armDelta.y) * Mathf.Rad2Deg;

        //Debug.Log(Mathf.Abs(_arm.transform.localEulerAngles.z) + Mathf.Abs(_forearm.transform.localEulerAngles.z));
        //Debug.Log($"Arm: {Mathf.Abs(_arm.transform.localEulerAngles.z)}");
        Debug.Log($"Forearm: {Mathf.Abs(_forearm.transform.localEulerAngles.z)}");

        if (!MiniGameFinish.MiniGameIsFinished)
        {
            CheckThreshold();

            if (_canLift && Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Push());
            }
        }

        if (MiniGameFinish.MiniGameIsFinished)
        {
            _forearm.freezeRotation = true;
        }

        //Debug.Log(_lifts);
    }

    private void CheckThreshold()
    {
        float angle = Mathf.Abs(_forearm.transform.localEulerAngles.z);
        //float angle = Mathf.Abs(_arm.transform.localEulerAngles.z) +
        //    Mathf.Abs(_forearm.transform.localEulerAngles.z);

        if (_canLift && (angle < _liftThreshold))
        {
            _lifts += 1;
            _canLift = false;
        }

        // Let the arm fall to rest position
        if (!_canLift && (angle > _restThreshold))
        {
            _canLift = true;
        }

        if (_lifts >= 2)
        {
            //_arm.freezeRotation = true;
            _forearm.freezeRotation = true;
            _finishEventChannel.Raise(gameObject);
        }
    }

    private IEnumerator Push()
    {
        return null;
        // Snap the object to the mouse cursor
        //_pusher.transform.position = Camera.main.ScreenToWorldPoint(
        //    new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        //_pusher.forceMagnitude = _pushAmount;
        //yield return new WaitForSeconds(.1f);
        ////yield return new WaitForFixedUpdate();
        //_pusher.forceMagnitude = 0;
    }
}
