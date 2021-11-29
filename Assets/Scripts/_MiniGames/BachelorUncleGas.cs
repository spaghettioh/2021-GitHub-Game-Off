using System.Collections;
using UnityEngine;

public class BachelorUncleGas : MonoBehaviour
{
    [SerializeField] private PointEffector2D _pusher;
    [SerializeField] private Transform _bachelor;
    [SerializeField] private Rigidbody2D _arm;
    [SerializeField] private FinishEventChannelSO _finishEventChannel;
    [SerializeField] private float _pushAmount;
    private float _threshold = .7f;

    [Range(0, 5)]
    public float leftRight;
    [Range(0, 5)]
    public float upDown;

    private void Update()
    {
        if (!MiniGameFinish.MiniGameIsFinished)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Push());
            }
        }

        UpdatePusherPosition();
        UpdateBachelorLean();
        CheckThreshold();
    }

    private void UpdatePusherPosition()
    {
        // Snap the object to the mouse cursor
        _pusher.transform.position = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20));
    }

    private void UpdateBachelorLean()
    {
        _bachelor.localPosition = new Vector3(
            _arm.transform.rotation.z * leftRight,
            Mathf.Abs(_arm.transform.rotation.z) * upDown,
            0);
    }

    private void CheckThreshold()
    {
        if (Mathf.Abs(_arm.transform.rotation.z) > _threshold ||
            MiniGameFinish.MiniGameIsFinished)
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
        _pusher.forceMagnitude = _pushAmount;
        yield return new WaitForSeconds(.1f);
        _pusher.forceMagnitude = 0;
    }
}
