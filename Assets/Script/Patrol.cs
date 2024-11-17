using UnityEngine;

public class Patrol : IState<enemi>
{
    // Start is called before the first frame update
    public void OnEnter(enemi e)
    {
        e.changeAnim("Run");
        SeekTarget(e);
                                                                                                
    }

    public void OnExecute(enemi e)
    {
        if (e.IsDestintion) 
        { 
            e.ChangeState(new IdleState());
        }
    }

    public void OnExit(enemi e)
    {

    }

    private void SeekTarget(enemi e)
    {
        Vector3 poin = levelManager.Ins.GetRandomPointNavmesh();

        e.SetDestination(poin);
    }
}
