using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotation : CannonState
{
    private float timeToShoot;
    private float timer = 0;
    private GameObject player;
    private Transform barrel;

    public CannonRotation(ICannon enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        player = GameObject.FindWithTag("Player");
        barrel = enemy.GetGameObject().transform.Find("Barrel");
        timeToShoot = enemy.GetSecondsToShoot();
    }

    public override void Exit()
    {
        //Debug.Log("El cañón ha dejado de apuntar");
    }

    public override void FixedUpdate()
    {
        //No hay FixedUpdate
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        float distance = Vector3.Distance(barrel.transform.position, player.transform.position);
        float shootingDistance = enemy.GetShootingDistance();
        if (distance > shootingDistance)
        {
            enemy.SetState(new CannonIdle(enemy));
        }
        else if (timer >= timeToShoot) enemy.SetState(new CannonShoot(enemy));
        //El cañón rota para mirar al jugador. Hay que encontrar el vector director de hacia donde mira.
        //Cabe destacar que el lanzamiento del proyectil es por fuerza, por lo que la bala no llegaría al jugador
        //si este estuviera muy lejos. Para ello, hacemos que el cañón apunte más "arriba" conforme más lejos esté el jugador
        float distanceVariation = distance/6.5f;
        Vector3 director = barrel.transform.position - player.transform.position;
        director -= new Vector3(0, distanceVariation, 0);
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, director);
        float rotationSpeed = enemy.GetRotationSpeed();
        barrel.transform.rotation = Quaternion.RotateTowards(barrel.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
