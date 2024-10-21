using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadExplosion : MonoBehaviour, IPooleableObject, IPrototype
{
    private ObjectPool pool;
    private void Awake()
    {
        pool = GameObject.FindObjectOfType<ObjectPool>();
        AddToPool();
        SetActive(false);
    }

    private void OnEnable()
    {
        Invoke("Release", 0.5f);
    }

    private void Release()
    {
        pool.Release(this);
    }

    public void AddToPool()
    {
        pool.Add(this);
    }

    public IPrototype Clone()
    {
        GameObject explosion = Instantiate(this.gameObject);
        explosion.GetComponent<IPooleableObject>().Reset();
        return explosion.GetComponent<IPrototype>();
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public string GetName()
    {
        return "ThreadExplosion";
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public void Reset()
    {

    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
