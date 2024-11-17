using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasSetting : MonoBehaviour
{
    [SerializeField] Button Resume;
    [SerializeField] Button MainMenu;
    [SerializeField] Button Exit;
    [SerializeField] GameObject PanelSetting;
    public Player player;
    void Start()
    {
        Resume.onClick.AddListener(() => resume());
        MainMenu.onClick.AddListener(() => mainMenu());
        Exit.onClick.AddListener(() => exit());
    }

    public void resume()
    {
        Time.timeScale = 1;
        PanelSetting.SetActive(false);
    }

    public void mainMenu()
    {
        GameController.Ins.comeHome3();
        PanelSetting.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void exit()
    {
        GameController.Ins.comeHome3();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PanelSetting.SetActive(false);
        Time.timeScale = 1;
    }
}
