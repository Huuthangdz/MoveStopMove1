using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] Transform hand;
    [SerializeField] Transform Pant;
    [SerializeField] Transform Hair;

    GameObject currentHair;
    GameObject currenWeapon;
    void Start()
    {

    }

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
        // Lấy vật liệu mới cho quần áo từ GameController
        Material newPantMaterial = GameController.Ins.GetCurrentPants(index);

        // Tạo một bản sao của vật liệu mới
        Material newMaterialInstance = new Material(newPantMaterial);

        // Lấy Renderer của đối tượng cần áp dụng vật liệu
        Renderer renderer = Pant.GetComponent<Renderer>();

        // Kiểm tra xem Renderer có tồn tại không
        if (renderer != null)
        {
            // Áp dụng vật liệu mới vào Renderer
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
