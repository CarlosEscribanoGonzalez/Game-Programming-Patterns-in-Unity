using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;


public class EnemyController : MonoBehaviour, IEnemy, IPrototype, IPooleableObject
{
    private IState currentState;
    public bool isPooleable = false; //Indica si pertenecen al object pool (invocables por el baúl)
    private FlyweightEnemy flyweight;
    public string enemyType;
    private bool canMove = true;
    private Transform playerTransform;
    private Rigidbody2D rb;
    private ObjectPool pool;
    private EnemyHealth health;
    private Animator anim;
    Spawner spawner;
    public void Awake()
    {
        anim = GetComponent<Animator>();
        flyweight = EnemyFactory.GetFlyWeightEnemy(enemyType);
        playerTransform = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        pool = GameObject.FindObjectOfType<ObjectPool>();
        spawner = GameObject.FindObjectOfType<Spawner>();
        health = GetComponent<EnemyHealth>();
        SetState(new IdleEnemy(this));
        if (!isPooleable) this.tag = "Enemy"; //Los pool de los enemigos se hacen por su tag. Hay que evitar hacer un pool a un enemigo que no es del baúl
        else
        {
            AddToPool();
            SetActive(false);
        }
    }

    private void Update()
    {
        currentState.Update();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    #region IEnemy
    public IState GetState()
    {
        return currentState;
    }
    public void SetState(IState state)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        // Set current state and enter
        currentState = state;
        currentState.Enter();
    }

    public void SetSpeed(float speed)
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    public float GetWalkingSpeed()
    {
        return flyweight.walkingSpeed;
    }

    public float GetChaseSpeed()
    {
        return flyweight.chasingSpeed;
    }

    public float GetAttackCooldown()
    {
        return flyweight.attackCooldown;
    }
    
    public void Attack() 
    {
        Vector3 director = GameObject.FindWithTag("Player").transform.position - this.transform.position;
        int direction = (int)Mathf.Sign(director.x);
        PlayerHealth pHealth = GameObject.FindObjectOfType<PlayerHealth>();
        pHealth.TakeDamage(flyweight.damage);
        pHealth.ApplyImpulse(flyweight.damage, direction);
    }

    public float GetIdleTime()
    {
        return flyweight.idleTime;
    }

    public float GetWanderTime()
    {
        return flyweight.wanderTime;
    }

    public float GetAttackingDistance()
    {
        return flyweight.attackingDistance;
    }

    public bool PlayerAtSight()
    {
        Vector3 distance = transform.position - playerTransform.position;
        if (Mathf.Abs(distance.x) <= flyweight.chasingXDistance && Mathf.Abs(distance.y) <= flyweight.chasingYDistance) return true;
        return false;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public void SetCanMove(bool c)
    {
        canMove = c;
    }

    public Animator GetAnimator()
    {
        return anim;
    }

    #endregion

    //Prototype para el spawner
    public IPrototype Clone()
    {
        GameObject newEnemy = Instantiate(this.gameObject);
        newEnemy.transform.parent = GameObject.FindWithTag("Spawner").transform;
        newEnemy.GetComponent<IPooleableObject>().Reset();
        return newEnemy.GetComponent<IPrototype>();
    }

    //ObjectPool:
    public void Reset()
    {
        canMove = true;
    }

    public string GetName()
    {
        return enemyType;
    }

    public void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

    public bool IsActive()
    {
        return this.gameObject.activeSelf;
    }

    public void AddToPool()
    {
        pool.Add(this);
    }
}
