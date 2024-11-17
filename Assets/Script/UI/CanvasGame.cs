using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasGame : MonoBehaviour
{
    [SerializeField] Button Setting;
    [SerializeField] GameObject PanelSetting;

    // Start is called before the first frame update
    void Start()
    {
        Setting.onClick.AddListener(() => ButtonSetting());
    }

    public void ButtonSetting()
    {
        GameController.Ins.joyStickoff();
        PanelSetting.SetActive(true);
        Time.timeScale = 0;
    }
}
