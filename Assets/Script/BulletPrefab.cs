
using UnityEngine;

public class BulletPrefab : MonoBehaviour
{
    [SerializeField] Transform head;

    GameObject currentBullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeBullet(int index)
    {
        if (currentBullet != null)
        {
            Destroy(currentBullet.gameObject);
        }
        currentBullet = Instantiate(GameController.Ins.GetCurrentWeapon(index), head);
    }
}
