using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour, ISubject<float>, IDamageTaker
{
    FlyweightEnemy flyweight;
    private float currentHealth;
    private Rigidbody2D rb;
    private ObjectPool pool;
    private readonly List<IObserver<float>> observers = new();

    private void Start()
    {
        flyweight = EnemyFactory.GetFlyWeightEnemy(GetComponent<EnemyController>().enemyType);
        currentHealth = flyweight.maxHealth; 
        rb = GetComponent<Rigidbody2D>();
        pool = GameObject.FindObjectOfType<ObjectPool>();
        StartObservers();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        NotifyObservers();
        StartCoroutine(KnockoutEnemy());
        if (currentHealth <= 0)
        {
            Invoke("Die", 0.1f);
        }
    }

    public void RestoreHP()
    {
        currentHealth = flyweight.maxHealth;
    }

    public void ApplyImpulse(float damage, int direction)
    {
        rb.AddForce(transform.right * direction * damage, ForceMode2D.Impulse);
    }

    IEnumerator KnockoutEnemy()
    {
        IEnemy enemy = GetComponent<IEnemy>();
        enemy.SetCanMove(false);
        enemy.SetState(new IdleEnemy(enemy));
        yield return new WaitForSeconds(flyweight.knockedoutTime);
        enemy.SetCanMove(true);
    }

    private void Die()
    {
        if(GetComponent<EnemyController>().enemyType == "Thingy")
        {
            IPooleableObject threadRemains = pool.Get("ThreadRemains", this.transform.position);
            if (threadRemains == null)
            {
                GameObject.FindObjectOfType<ThreadRemains>().Clone();
                pool.Get("ThreadRemains", this.transform.position);
            }
        }
        else
        {
            IPooleableObject healingBall = pool.Get("HealingBall", this.transform.position);
            if (healingBall == null)
            {
                GameObject.FindObjectOfType<HealingBall>().Clone();
                pool.Get("HealingBall", this.transform.position);
            }
        }

        if (GetComponent<EnemyController>().isPooleable)
        {
            RestoreHP();
            pool.Release(GetComponent<EnemyController>());
            GameObject.FindObjectOfType<Spawner>().DecreaseEnemies();
        }
        else gameObject.SetActive(false);
    }


    //Observer:
    public void AddObserver(IObserver<float> observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver<float> observer)
    {
        observers.Remove(observer);
    }

    public void StartObservers()
    {
        foreach (IObserver<float> o in observers)
        {
            o.StartObserver(currentHealth);
        }
    }

    public void NotifyObservers()
    {
        foreach (IObserver<float> o in observers)
        {
            o.UpdateObserver(currentHealth);
        }
    }
}
