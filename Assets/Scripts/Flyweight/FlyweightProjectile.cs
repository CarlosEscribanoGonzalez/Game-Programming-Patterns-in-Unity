using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyweightProjectile
{
    public float speed;
    public float rotationSpeed;
    public float damage;

    public FlyweightProjectile(string type)
    {
        if(type == "ThreadBall")
        {
            speed = 30;
            rotationSpeed = 600;
            damage = 15;
        } 
        else if(type == "CannonBall")
        {
            speed = 35;
            rotationSpeed = 600;
            damage = 20;
        }
    }

    public void ProcessCollision(Collision2D collision, Vector3 position)
    {
        IDamageTaker damagedObject = collision.gameObject.GetComponent<IDamageTaker>();
        if (damagedObject != null)
        {
            Vector3 director = collision.gameObject.transform.position - position;
            int direction = (int)Mathf.Sign(director.x);
            damagedObject.TakeDamage(damage);
            damagedObject.ApplyImpulse(damage, direction);
        }
    }
}
