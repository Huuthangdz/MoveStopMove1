using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] Button Play;
    [SerializeField] Button Weapon;
    [SerializeField] Button Skin;

     void Start()
    {
        Play.onClick.AddListener(() => PlayGame());
        Weapon.onClick.AddListener(() => ChangeWeapon());
        Skin.onClick.AddListener(() => ChangeSkin());
    }
    private void PlayGame()
    {
        GameController.Ins.Main();
        levelManager.Ins.OnInit();
    }
    private void ChangeWeapon()
    {
        GameController.Ins.Weapon();
    }

    private void ChangeSkin()
    {
        GameController.Ins.ChangeSkin();
    }
}
