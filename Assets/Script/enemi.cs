using UnityEngine;
using UnityEngine.AI;
public class enemi : character
{
    public NavMeshAgent agent;
    public bool IsDestintion => (Mathf.Abs(destionation.x - transform.position.x) + Mathf.Abs(destionation.z - transform.position.z)) < 0.2f;
    public float distance1 => (Mathf.Abs(destionation.x - transform.position.x) + Mathf.Abs(destionation.z - transform.position.z));
    public Vector3 VECTOR1 => destionation;
    public Vector3 VECTOR2 => transform.position;


    public float distance => Vector3.Distance(destionation, transform.position.x * Vector3.right +
        Vector3.up * 3.384139f + Vector3.forward * transform.position.z);
    public CounterTime Counter => counter;
    public bool isDeath = false;

    private Vector3 destionation;
    private CounterTime counter = new CounterTime();

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        //destionation = transform.position;
        ChangeState(new Patrol());
        changeAnim("Run");
        switch (Random.Range(1, 5))
        {
            case 1: ChangeColor(ColorType.red); break;
            case 2: ChangeColor(ColorType.yellow); break;
            case 3: ChangeColor(ColorType.green); break;
            case 4: ChangeColor(ColorType.icon); break;

            default: Debug.Log("đần"); break;
        }
    }
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        counter.Excute();
    }

    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destionation = position;
        destionation.y = 0.5f;
        agent.SetDestination(position);
    }

    IState<enemi> currentState;



    public void ChangeState(IState<enemi> state)
    {
        if (!isDeath)
        {
            if (currentState != null)
            {
                currentState.OnExit(this);
            }
            currentState = state;
            if (currentState != null)
            {
                currentState.OnEnter(this);
            }
        }
    }
    public override void OnDead()
    {
        isDeath = true;
        base.OnDead();
        ChangeState(null);
        agent.enabled = false;
        counter.Start(Wait, 0.5f);
        Invoke(nameof(DisableSelf), 3f);
        levelManager.Ins.AliveBot();
    }

    private void Wait()
    {
        GetComponent<CapsuleCollider>().enabled = false;
    }

    private void DisableSelf()
    {
        gameObject.SetActive(false);
        Invoke("OnDestroy", 1f);
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
    public override void OnAttack()
    {
        base.OnAttack();
        character target = GetTargetInRange();
        if ( target != null)
        {
            changeAnim("attack");
            GameObject bulletPrefab = BulletPrefab;
            GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
            Vector3 direction = (new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position).normalized;
            rb.AddForce(direction * 5, ForceMode.Impulse);
            Bullet bullet = bulletInstance.GetComponent<Bullet>();
            bullet.OnInit(this, target.transform);
            RemoveTaget(target);
        }
    }

    public override void changeAnim(string animName)
    {
        base.changeAnim(animName);

    }
    public void OnMoveStop()
    {
        agent.enabled = false;
        changeAnim("Idle");
    }

    public override void AddTarget(character target)
    {
        base.AddTarget(target);
        if (IsInCameraView(transform.position))
        {
            if (Random.Range(0, 10) < 7)
            {
                ChangeState(new attackState());
                Invoke(nameof(ChangeStateAfterAttack), 1f);
            }
        }
    }
    private bool IsInCameraView(Vector3 position)
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(position);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
               viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
               viewportPoint.z > 0;
    }
    private void ChangeStateAfterAttack()
    {

        if (!isDeath)
        {
            if (Random.Range(0, 2) == 0)
            {
                ChangeState(new IdleState());
            }
            else
            {
                ChangeState(new Patrol());
            }
        }
    }
    public override void OnInit()
    {
        
        base.OnInit();
    }
}
