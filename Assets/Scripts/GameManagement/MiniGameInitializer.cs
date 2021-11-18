using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameInitializer : MonoBehaviour
{
    [Header("PersistentManagers")]
    [SerializeField] private string _sceneName = "PersistentManagers";

    private void OnEnable()
    {
        SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
    }
}
