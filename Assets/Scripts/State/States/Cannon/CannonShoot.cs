using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonShoot : CannonState
{
    private ObjectPool pool;
    private float cooldown;
    private float timer = 0;
    public CannonShoot(ICannon enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        cooldown = enemy.GetCooldown();
        pool = GameObject.FindObjectOfType<ObjectPool>();
        IPooleableObject cannonBall = pool.Get("CannonBall");
        if(cannonBall == null)
        {
            GameObject.FindObjectOfType<CannonBall>().Clone();
            pool.Get("CannonBall");
        }
    }

    public override void Exit()
    {
        //Debug.Log("El cañón ha recargado");
    }

    public override void FixedUpdate()
    {
        //No hay FixedUpdate
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cooldown) enemy.SetState(new CannonIdle(enemy));
    }
}
