using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<enemi>
{
    void OnEnter(enemi e);

    void OnExecute(enemi e);

    void OnExit(enemi e);
}
