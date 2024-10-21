using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject bigRobotPrefab;
    [SerializeField] private GameObject smallThingyPrefab;
    [SerializeField] private int maxEnemies;
    [SerializeField] private float interval;
    private bool canSpawn = true;
    private int actualEnemies = 0;
    private ObjectPool pool;
    void Start()
    {
        pool = GameObject.FindObjectOfType<ObjectPool>();
    }

    void Update()
    {
        if (actualEnemies < maxEnemies && canSpawn)
        {
            SpawnEnemies();
            StartCoroutine(Cooldown());
            actualEnemies++;
        }
    }

    IEnumerator Cooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(interval);
        canSpawn = true;
    }

    private void SpawnEnemies()
    {
        int num = Random.Range(0, 2);
        string enemyType = (num == 0) ? "Robot" : "Thingy";
        IPooleableObject newEnemy = pool.Get(enemyType, this.transform.position);
        if(newEnemy == null)
        {
            GameObject.FindWithTag(enemyType).GetComponent<EnemyController>().Clone();
            pool.Get(enemyType, this.transform.position);
        }
    }
   
    public void DecreaseEnemies()
    {
        actualEnemies--;
    }
}

