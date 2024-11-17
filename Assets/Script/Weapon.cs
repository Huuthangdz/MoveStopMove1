using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        child.SetActive(true);
    }
    public void Throw()
    {
        Invoke(nameof(OnEnable),1f);
    }
}
