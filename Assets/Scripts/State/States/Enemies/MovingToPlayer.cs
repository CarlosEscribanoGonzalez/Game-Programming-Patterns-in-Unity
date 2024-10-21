using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToPlayer : EnemyState
{
    private Transform enemyTransform;
    private Transform playerTransform;
    public MovingToPlayer(IEnemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        enemyTransform = enemy.GetGameObject().transform;
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        //Velocidad del enemigo:
        if (playerTransform.position.x > enemyTransform.position.x)
        {
            enemy.SetSpeed(enemy.GetChaseSpeed());
            enemy.GetGameObject().GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            enemy.SetSpeed(-enemy.GetChaseSpeed());
            enemy.GetGameObject().GetComponent<SpriteRenderer>().flipX = false;
        }
        //Condiciones de cambio de estado:
        float distance = Vector3.Distance(playerTransform.position, enemyTransform.position);
        if (!enemy.PlayerAtSight())
        {
            enemy.GetAnimator().SetBool("Move", false);
            enemy.SetState(new IdleEnemy(enemy));
        }
        else if (distance <= enemy.GetAttackingDistance())
        {
            enemy.GetAnimator().SetBool("Move", false);
            enemy.SetState(new AttackingPlayer(enemy));
        }
    }

 
}
