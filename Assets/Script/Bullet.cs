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

    public void OnInit(character Character,Transform target)
    {
        this.character = Character;
        this.target = target;
        transform.forward = (target.position - transform.position).normalized;
        //transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        counterTime.Start(deactiveBullet,1f);
    }

    public void deactiveBullet()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")  && other.gameObject != character.gameObject)
        {
            character.UpdatePoints();
            other.GetComponent<character>().OnDead();
        }
    } 
}
