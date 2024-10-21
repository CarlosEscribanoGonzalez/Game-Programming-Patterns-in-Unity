using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CannonIdle : CannonState
{
    private GameObject player;
    public CannonIdle(ICannon enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        player = GameObject.FindWithTag("Player");
    }

    public override void Exit()
    {
        //Debug.Log("Cañón apuntando al jugador");
    }

    public override void FixedUpdate()
    {
        //No hay FixedUpdate
    }

    public override void Update()
    {
        float distance = Vector3.Distance(enemy.GetGameObject().transform.position, player.transform.position);
        float shootingDistance = enemy.GetShootingDistance();
        if (distance < shootingDistance)
        {
            enemy.SetState(new CannonRotation(enemy));
        } 
    }
}
