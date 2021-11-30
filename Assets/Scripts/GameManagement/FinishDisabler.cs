using UnityEngine;

public class FinishDisabler : MonoBehaviour
{
    [SerializeField] private FinishEventChannelSO _finishEventChannel;

    private void OnEnable()
    {
        _finishEventChannel.OnFinished += DisableMe;
    }

    private void OnDisable()
    {
        _finishEventChannel.OnFinished -= DisableMe;
    }

    private void DisableMe(GameObject unused)
    {
        gameObject.SetActive(false);
    }
}
