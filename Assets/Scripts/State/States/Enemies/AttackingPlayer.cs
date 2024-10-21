using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingPlayer : EnemyState
{
    private float attackCooldown;
    private float timer;

    public AttackingPlayer(IEnemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        attackCooldown = enemy.GetAttackCooldown();
        timer = 0;
        enemy.SetSpeed(0); //Opcional que se quede quieto o no al atacar. Se puede quitar
        enemy.Attack();
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
        if (timer >= attackCooldown && enemy.PlayerAtSight())
        {
            enemy.GetAnimator().SetBool("Move", true);
            enemy.SetState(new MovingToPlayer(enemy));
        } 
        else if(timer >= attackCooldown && !enemy.PlayerAtSight())
        {
            enemy.GetAnimator().SetBool("Move", false);
            enemy.SetState(new IdleEnemy(enemy));
        }
    }

}
