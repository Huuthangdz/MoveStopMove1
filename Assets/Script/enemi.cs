using System.Collections;
using System.Collections.Generic;
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
        //switch (Random.Range(1, 5))
        //{
        //    case 1: ChangeColorPant(ColorType.red); break;
        //    case 2: ChangeColorPant(ColorType.yellow); break;
        //    case 3: ChangeColorPant(ColorType.green); break;
        //    case 4: ChangeColorPant(ColorType.icon); break;

        //    default: Debug.Log("đần"); break;
        //}

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
        counter.Start(wait, 3f);
        Invoke(nameof(disableSelf), 3f);
    }

    private void wait()
    {
        GetComponent<CapsuleCollider>().enabled = false;
    }

    private void disableSelf()
    {
        gameObject.SetActive(false);
    }
    public override void OnAttack()
    {
        base.OnAttack();
        target = GetTargetInRange();
        changeAnim("attack");
        counter.Start(Throw, 0.5f);
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
        if (Random.Range(0, 2) == 0)
        {
            ChangeState(new attackState());
            Invoke(nameof(ChangeStateAfterAttack), 1f);
        }
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
