using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UFOlogistText : MonoBehaviour
{
    [SerializeField] private FinishEventChannelSO _finishEventChannel;
    private Image _mask;

    private float _progress = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _mask = GetComponent<Image>();
        _mask.fillAmount = _progress;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Finish.MiniGameIsFinished && _progress < 1f)
        {
            //_progress += Time.deltaTime;

            if (_progress > 1f)
            {
                _progress = 1f;
            }

            _mask.fillAmount = _progress;

            if (_progress == 1f)
            {
                _finishEventChannel.Raise(gameObject);
            }
        }
    }
}
