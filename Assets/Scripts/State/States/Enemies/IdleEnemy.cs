using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleEnemy : EnemyState
{
    private float idleTime;
    private float timer;
    

    public IdleEnemy(IEnemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        
        idleTime = enemy.GetIdleTime();
        enemy.SetSpeed(0);
        
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (enemy.GetCanMove())
        {
            if (enemy.PlayerAtSight())
            {
                enemy.GetAnimator().SetBool("Move", true);
                enemy.SetState(new MovingToPlayer(enemy));
            }
            else if (timer >= idleTime)
            {
                enemy.GetAnimator().SetBool("Move", true);
                enemy.SetState(new WanderingEnemy(enemy));
            }
            enemy.SetSpeed(0);
        }
    }
}
