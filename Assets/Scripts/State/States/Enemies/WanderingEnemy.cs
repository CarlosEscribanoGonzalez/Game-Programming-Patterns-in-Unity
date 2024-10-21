using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WanderingEnemy : EnemyState
{
    private float wanderTime;
    private float timer;
    private int direction;

    public WanderingEnemy(IEnemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        wanderTime = enemy.GetWanderTime();
        timer = wanderTime;
        int ran = Random.Range(0, 2);
        if (ran == 0) direction = -1;
        else direction = 1;
        direction *= -1; //Se invierte el signo a direction para que cuando tenga que girar invierta la velocidad
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
        if (enemy.PlayerAtSight())
        {
            enemy.GetAnimator().SetBool("Move", true);
            enemy.SetState(new MovingToPlayer(enemy));
        }
        else if(timer >= wanderTime)
        {
            enemy.SetSpeed(direction * enemy.GetWalkingSpeed());
            timer = 0;
            direction *= -1;
        }
        if (direction > 0) enemy.GetGameObject().GetComponent<SpriteRenderer>().flipX = false;
        else enemy.GetGameObject().GetComponent<SpriteRenderer>().flipX = true;
    }
}
