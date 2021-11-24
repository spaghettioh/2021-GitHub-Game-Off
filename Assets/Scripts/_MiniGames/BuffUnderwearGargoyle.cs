using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUnderwearGargoyle : MonoBehaviour
{
    [SerializeField] private PointEffector2D _pusher;
    [SerializeField] private Rigidbody2D _arm;
    [SerializeField] private Rigidbody2D _forearm;
    [SerializeField] private FinishEventChannelSO _finishEventChannel;
    [SerializeField] private float _pushAmount;

    private float _liftThreshold = 430f;
    private float _restThreshold = 630f;

    private int _lifts = 0;

    // Forces the arm to fall to resting position
    private bool _canPush = true;

    private enum State
    {
        Lifting,
        Falling,
    }
    private State state = State.Lifting;

    private void Update()
    {
        //Debug.Log(Mathf.Abs(_arm.transform.localEulerAngles.z) + Mathf.Abs(_forearm.transform.localEulerAngles.z));
        //Debug.Log($"Arm: {Mathf.Abs(_arm.transform.localEulerAngles.z)}");
        //Debug.Log($"Forearm: {Mathf.Abs(_forearm.transform.localEulerAngles.z)}");

        if (!MiniGameFinish.MiniGameIsFinished)
        {
            CheckThreshold();

            if (_canPush && Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Push());
            }
        }

        //Debug.Log(_lifts);
    }

    private void CheckThreshold()
    {
        float angle = Mathf.Abs(_arm.transform.localEulerAngles.z) +
            Mathf.Abs(_forearm.transform.localEulerAngles.z);

        if (_canPush && (angle < _liftThreshold))
        {
            _lifts += 1;
            _canPush = false;
        }

        // Let the arm fall to rest position
        if (!_canPush && (angle > _restThreshold))
        {
            _canPush = true;
        }

        if (_lifts >= 2)
        {
            _arm.freezeRotation = true;
            _forearm.freezeRotation = true;
            _finishEventChannel.Raise(gameObject);
        }
    }

    private IEnumerator Push()
    {
        // Snap the object to the mouse cursor
        //_pusher.transform.position = Camera.main.ScreenToWorldPoint(
        //    new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        _pusher.forceMagnitude = _pushAmount;
        yield return new WaitForSeconds(.1f);
        //yield return new WaitForFixedUpdate();
        _pusher.forceMagnitude = 0;
    }
}
