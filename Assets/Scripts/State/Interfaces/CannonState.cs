using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CannonState : IState
{
    protected ICannon enemy;

    public CannonState(ICannon enemy)
    {
        this.enemy = enemy;
    }

    public abstract void Enter();

    public abstract void Exit();


    public abstract void FixedUpdate();

    public abstract void Update();
}
