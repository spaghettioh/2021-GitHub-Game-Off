using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void LoadAScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
