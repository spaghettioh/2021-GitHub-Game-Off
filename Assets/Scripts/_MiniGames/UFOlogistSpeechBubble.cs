using UnityEngine;

public class UFOlogistSpeechBubble : MonoBehaviour
{
    [SerializeField] private Transform _handEast;
    private float _handEastPrevious;
    [SerializeField] private Transform _handWest;
    private float _handWestPrevious;
    private float _totalRotation = 0f;


    [SerializeField] private Animator _textMask;
    [SerializeField] private float _winningRotationAmount;
    [SerializeField] private FinishEventChannelSO _finishEventChannel;

    private float _progress = 0f;
    private HingeJoint2D _handEastJoint;
    private HingeJoint2D _handWestJoint;

    private void Start()
    {
        _handEastJoint = _handEast.GetComponent<HingeJoint2D>();
        _handWestJoint = _handWest.GetComponent<HingeJoint2D>();
    }

    private void Update()
    {
        float handEastCurrent;
        float handEastDelta;
        float handWestCurrent;
        float handWestDelta;

        handEastCurrent = _handEast.rotation.z * Mathf.Rad2Deg;
        handEastDelta = Mathf.Abs(handEastCurrent - _handEastPrevious);
        _handEastPrevious = handEastCurrent;

        handWestCurrent = _handWest.rotation.z * Mathf.Rad2Deg;
        handWestDelta = Mathf.Abs(handWestCurrent - _handWestPrevious);
        _handWestPrevious = handWestCurrent;

        _totalRotation += (handEastDelta + handWestDelta);

        if (!MiniGameFinish.InteractionsDisabled && _progress < 1f)
        {
            _progress = _totalRotation / _winningRotationAmount;

            if (_progress > 1f)
            {
                _progress = 1f;
            }

            _textMask.SetFloat("Progress", _progress);

            if (_progress >= 1f)
            {
                _finishEventChannel.Raise(gameObject);
            }
        }

        // Bug out
        if (_progress >= 1 && MiniGameFinish.InteractionsDisabled)
        {
            _handEastJoint.useMotor = true;
            _handWestJoint.useMotor = true;
        }
    }
}
