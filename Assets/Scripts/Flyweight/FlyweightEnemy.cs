using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyweightEnemy
{
    //Comunes a todos los enemigos:
    public float idleTime = 3;
    public float wanderTime = 5;
    public float attackCooldown = 0.5f;
    public float chasingXDistance = 14;
    public float chasingYDistance = 7;
    //Privados de cada enemigo:
    public float walkingSpeed;
    public float chasingSpeed;
    public float attackingDistance;
    public float damage;
    public float maxHealth;
    public float knockedoutTime;
    public FlyweightEnemy(string type)
    {
        if(type == "Robot")
        {
            walkingSpeed = 1.5f;
            chasingSpeed = 3;
            attackingDistance = 2;
            damage = 40; 
            maxHealth = 70;
            knockedoutTime = 0.1f;
        }
        else if(type == "Thingy")
        {
            walkingSpeed = 3f;
            chasingSpeed = 6;
            attackingDistance = 2f;
            damage = 20; 
            maxHealth = 30;
            knockedoutTime = 0.4f;
        }
    }
}
