using UnityEngine;

public class Bullet : MonoBehaviour
{
    //ban den dau
    private Transform target;

    //nguoi ban vien dan
    private character character;
    [SerializeField] float speed = 5f;
    [SerializeField] Transform child;

    CounterTime counterTime = new CounterTime();
    void Start()
    {
        OnInit(character, target);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        child.Rotate(Vector3.up * -6f, Space.Self);
        counterTime.Excute();
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
    public void OnInit(character Character,Transform target)
    {
        this.character = Character;
        this.target = target;
        Vector3 direction = new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position;
        transform.forward = direction.normalized;
        transform.position = new Vector3(transform.position.x, target.position.y + 1f, transform.position.z);
        counterTime.Start(deactiveBullet,1.5f);
    }

    public void deactiveBullet()
    {
        gameObject.SetActive(false);    
        Invoke("OnDestroy", 1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject != character.gameObject) 
        {
            character.UpdatePoints();
            other.GetComponent<character>().OnDead();
        } 
    } 
}
