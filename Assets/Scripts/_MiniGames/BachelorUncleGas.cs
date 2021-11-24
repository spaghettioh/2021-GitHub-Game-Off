using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BachelorUncleGas : MonoBehaviour
{
    [SerializeField] private PointEffector2D _pusher;
    [SerializeField] private Rigidbody2D _arm;
    [SerializeField] private FinishEventChannelSO _finishEventChannel;
    [SerializeField] private float _pushAmount;
    private float _threshold = .7f;

    private void Update()
    {
        if (!MiniGameFinish.MiniGameIsFinished)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Push());
            }
        }

        CheckThreshold();
    }

    private void CheckThreshold()
    {
        if (Mathf.Abs(_arm.transform.rotation.z) > _threshold)
        {
            if (!MiniGameFinish.MiniGameIsFinished)
            {
                _finishEventChannel.Raise(gameObject);
            }

            _arm.freezeRotation = true;
        }
    }

    private IEnumerator Push()
    {
        // Snap the object to the mouse cursor
        _pusher.transform.position = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        _pusher.forceMagnitude = _pushAmount;
        yield return new WaitForSeconds(.1f);
        _pusher.forceMagnitude = 0;
    }
}
