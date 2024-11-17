using System.Collections;
using System.Collections.Generic;
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
        shop.onClick.AddListener(() => comeShop());
    }

    public void ComeMain()
    {
        canvasDie.SetActive(false);
        GameController.Ins.comeHome3();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void comeShop()
    {
        GameController.Ins.ChangeSkin();
        canvasDie.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
