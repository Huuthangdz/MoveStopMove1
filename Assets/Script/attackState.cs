using UnityEngine;

public class attackState : IState<enemi>
{
    public void OnEnter(enemi e)
    {
        e.OnMoveStop();
        e.OnAttack();
    }

    public void OnExecute(enemi e)
    {

    }

    public void OnExit(enemi e)
    {

    }
}
