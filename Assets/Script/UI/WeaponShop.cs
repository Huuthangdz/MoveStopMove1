using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class WeaponShop : MonoBehaviour
{
    [SerializeField] Button ComeHome;
    [SerializeField] Button next;
    [SerializeField] Button previous;
    [SerializeField] Button pick;
    [SerializeField] Button equiped;
    [SerializeField] Transform parent;
    [SerializeField] GameObject player;
    Text textField;
    public GameObject[] textWeapon;
    public Skin skin;
    private int weapon_index = 0;
    private int total_weapon = 0;
    private GameObject weapon;
    void Start()
    {
        ComeHome.onClick.AddListener(() => HomeMainMenu());
        weapon_index = PlayerPrefs.GetInt("Weapon");
        total_weapon = GameController.Ins.total_weapon;
        next.onClick.AddListener(() => NextWeapon());
        previous.onClick.AddListener(() => BackWeapon());
        pick.onClick.AddListener(() => ChangWeapon());
        textField = GameObject.Find("TextWeapon").GetComponent<Text>();
        textWeapon[0].SetActive(false);
    }

    private void InitWeapon(int indexs)
    {
        weapon_index = indexs;
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
        if (indexs == PlayerPrefs.GetInt("Weapon"))
        {
            pick.gameObject.SetActive(false);
            equiped.gameObject.SetActive(true);
        }
        else
        {
            pick.gameObject.SetActive(true);
            equiped.gameObject.SetActive(false);
        }
    }
    void Update()   
    {
        if (weapon_index == PlayerPrefs.GetInt("Weapon"))
        {
            pick.gameObject.SetActive(false);
            equiped.gameObject.SetActive(true);
        }
        else
        {
            pick.gameObject.SetActive(true);
            equiped.gameObject.SetActive(false);
        }
        weapon = Instantiate(GameController.Ins.GetCurrentWeapon(weapon_index), parent.position, Quaternion.identity, parent);
        weapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
        textWeapon[weapon_index].SetActive(true);
    }
    public void text(int index)
    {
        weapon_index = index;
    }
    private void ChangWeapon()
    {
        PlayerPrefs.SetInt("Weapon", weapon_index);
        player.GetComponent<character>().ChangeWeapon(weapon_index);
        InitWeapon(weapon_index);
    }
    private void NextWeapon()
    {
        int index = weapon_index;
        weapon_index++;
        if(weapon_index == total_weapon )
        {
            weapon_index = 0;
        }
        InitWeapon(weapon_index);
        textWeapon[index].SetActive(false);
        textWeapon[weapon_index].SetActive(true);
    }
    private void BackWeapon()
    {
        int index = weapon_index;
        weapon_index--;
        if(weapon_index < 0)
        {
            weapon_index = total_weapon - 1 ;
        }
        InitWeapon(weapon_index);
        textWeapon[index].SetActive(false);
        textWeapon[weapon_index].SetActive(true);
    }
    public void HomeMainMenu()
    {
        GameController.Ins.comeHome1();
    }
}
