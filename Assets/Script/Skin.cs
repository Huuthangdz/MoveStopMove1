using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] Transform hand;
    [SerializeField] Transform Pant;
    [SerializeField] Transform Hair;

    GameObject currentHair;
    GameObject currenWeapon;

    public void ChangeWeapon(int index)
    {
        if (currenWeapon != null)
        {
            Destroy(currenWeapon.gameObject);
        }
        currenWeapon = Instantiate(GameController.Ins.GetCurrentWeapon(index), hand);
    }
    public void ChangePant(int index)
    {
        Material newPantMaterial = GameController.Ins.GetCurrentPants(index);
        Material newMaterialInstance = new Material(newPantMaterial);
        Renderer renderer = Pant.GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.material = newMaterialInstance;
        }
    }
    public void ChangeHair(int index)
    {
        if (currentHair != null)
        {
            Destroy(currentHair.gameObject);
        } 
        currentHair = Instantiate(GameController.Ins.GetCurrentHair(index),Hair);
    }
}
