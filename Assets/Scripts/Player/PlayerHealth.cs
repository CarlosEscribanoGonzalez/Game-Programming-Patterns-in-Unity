using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, ISubject<float>, ISaveableObject, IDamageTaker
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float protectionTime = 2; //Tiempo de invencibilidad tras haber sido golpeado
    [SerializeField] private float knockedTime = 0.5f; //Tiempo tras recibir un golpe en el que el jugador no puede moverse
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material blinkMaterial;
    private Transform playerTransform;
    private SpriteRenderer pRenderer;
    private float health;
    private bool vulnerable = true;
    private readonly List<IObserver<float>> observers = new();
    private bool dirty = false;


    private void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        pRenderer = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (!dirty) health = maxHealth; //Quiere decir que si no se ha hecho un Restore en el awake significa que no hay datos guardados. Vida máxima
        StartObservers();
    }

    public float GetCurrentHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (vulnerable)
        {
            health -= damage;
            NotifyObservers();
            if (health <= 0)
            {
                GameObject.FindObjectOfType<SceneChanger>().GameOver();
                return;
            }
            StartCoroutine(GiveProtection());
            StartCoroutine(Blink());
        }
    }

    public void ApplyImpulse(float damage, int direction)
    {
        if(vulnerable) GameObject.FindObjectOfType<PlayerController>().ApplyImpulse(damage, direction, knockedTime);
    }

    public void HealPlayer(float h)
    {
        health += h;
        if (health > maxHealth) health = maxHealth;
        NotifyObservers();
    }

    IEnumerator GiveProtection()
    {
        yield return new WaitForSeconds(0.1f);
        vulnerable = false;
        yield return new WaitForSeconds(protectionTime);
        vulnerable = true;
    }

    IEnumerator Blink()
    {
        float blinksNum = 3;
        float blinkDuration = protectionTime / blinksNum / 2;
        for(int i = 0; i < blinksNum; i++)
        {
            pRenderer.material = blinkMaterial;
            yield return new WaitForSeconds(blinkDuration);
            pRenderer.material = normalMaterial;
            yield return new WaitForSeconds(blinkDuration);
        }
        pRenderer.material = normalMaterial;
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
            o.StartObserver(health);
        }
    }

    public void NotifyObservers()
    {
        dirty = true;
        foreach(IObserver<float> o in observers)
        {
            o.UpdateObserver(health);
        }
    }

    //DirtyFlag:
    public bool IsDirty()
    {
        return dirty;
    }

    public void SetDirty(bool d)
    {
        dirty = d;
    }

    public object GetData()
    {
        return new HealthData
        {
            healthAmount = health,
            positionX = playerTransform.position.x,
            positionY = playerTransform.position.y
        };
    }

    public void RestoreData(object data)
    {
        HealthData hData = (HealthData)data;
        health = hData.healthAmount;
        GameObject.FindObjectOfType<PlayerController>().transform.position = new Vector3(hData.positionX, hData.positionY, 0);
        dirty = true;
    }
}
