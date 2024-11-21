using UnityEngine;
    

public class GameController : Singleton<GameController>
{
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject joyStick;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject weaponCanvas;
    [SerializeField] GameObject SkinCanvas;
    //[SerializeField] GameObject[] ListBullet;
    [SerializeField] Material[] ListPants;
    [SerializeField] GameObject[] ListWeapon;
    [SerializeField] GameObject[] ListHair;
    private int weapon_index = 0;
    private int pantsIndexs = 0;
    private int hair_index = 0;
    public int total_hair => ListHair.Length;
    public int total_weapon => ListWeapon.Length;
    public int total_pants => ListPants.Length;
   
    void Start()
    {
        if(!PlayerPrefs.HasKey("Weapon"))
        {
            PlayerPrefs.SetInt("Weapon",weapon_index);
        } 
        else
        {
            weapon_index = PlayerPrefs.GetInt("Weapon");
        }

        if (!PlayerPrefs.HasKey("Pants"))
        {
            PlayerPrefs.SetInt("Pants", pantsIndexs);
        }
        else
        {
            pantsIndexs = PlayerPrefs.GetInt("Pants");
        }

        if (!PlayerPrefs.HasKey("Hair"))
        {
            PlayerPrefs.SetInt("Hair", hair_index);
        } 
        else
        {
            hair_index = PlayerPrefs.GetInt("Hair");
        }
    }

    public GameObject GetCurrentWeapon(int index)
    {
        return ListWeapon[index];
    }
    //public GameObject GetCurrentBullet(int index)
    //{
    //    return ListBullet[index];
    //}
    public Material GetCurrentPants(int index)
    {
        return ListPants[index];
    }
    public GameObject GetCurrentHair(int index)
    {
        return ListHair[index];
    }
    public void Main()
    {
        joyStick.SetActive(true);
        gameCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void Weapon()
    {
        joyStick.SetActive(false);
        gameCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);
        weaponCanvas.SetActive(true);
    }

    public void comeHome1()
    {
        mainMenuCanvas.SetActive(true);
        weaponCanvas.SetActive(false);
        joyStick.SetActive(false);
    }
    public void comeHome2()
    {
        mainMenuCanvas.SetActive(true);
        SkinCanvas.SetActive(false);
        joyStick.SetActive(false);
    }
    public void comeHome3()
    {
        mainMenuCanvas.SetActive(true);
        joyStick.SetActive(false);
    }
    public void PlayerDead()
    {
        joyStick.SetActive(false);
    }
    public void ChangeSkin()
    {
        SkinCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }
    public void joyStickoff()
    {
        joyStick.SetActive(false);
    }
}
