using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(int _sceneNum)
    {
        SceneManager.LoadScene(_sceneNum);
    }
    public void ExitApp()
    {
        Application.Quit();
    }
}
