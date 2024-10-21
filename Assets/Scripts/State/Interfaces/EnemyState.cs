using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : IState
{
    protected IEnemy enemy;

    public EnemyState(IEnemy enemy)
    {
        this.enemy = enemy;
    }

    public abstract void Enter();

    public abstract void Exit();


    public abstract void FixedUpdate();

    public abstract void Update();
    
}
