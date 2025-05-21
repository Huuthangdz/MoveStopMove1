
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasDie : MonoBehaviour
{
    [SerializeField] Button mainMenu;
    [SerializeField] Button shop;
    [SerializeField] GameObject canvasDie;
    void Start()
    {
        mainMenu.onClick.AddListener(() => ComeMain());
        GameController.Ins.TurnOffSettingScene();
    }

    public void ComeMain()
    {
        canvasDie.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
