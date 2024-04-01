using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickPrefab : MonoBehaviour
{
    [SerializeField] Button button;
    private void Start()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        controller.GetComponent<FromWeb>().WatchID(button.gameObject);
        controller.GetComponent<SceneChanger>().ChangeScene(2);
    }
}
