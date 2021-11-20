using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneList : MonoBehaviour
{
    [SerializeField] private LoadEventChannelSO _sceneEventChannel;

    private TMP_Dropdown _scenesDropdown;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(SceneManager.sceneCountInBuildSettings);
        _scenesDropdown = GetComponent<TMP_Dropdown>();
        List<string> scenes = new List<string>();
        for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            //Debug.Log(SceneUtility.GetScenePathByBuildIndex(i));
            scenes.Add(SceneUtility.GetScenePathByBuildIndex(i));
        }
        //SceneManager.GetAllScenes();
        _scenesDropdown.ClearOptions();
        _scenesDropdown.AddOptions(scenes);
    }

    // Update is called once per frame
    void Update()
    {
        //_scenesDropdown.OnSelect( = _sceneEventChannel.LoadScene(_scenesDropdown.options[_scenesDropdown.value].ToString());
    }
}
