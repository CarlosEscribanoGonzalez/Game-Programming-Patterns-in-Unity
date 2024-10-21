using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBall : MonoBehaviour, IPooleableObject, IPrototype, IPickable
{
    [SerializeField] private float healNum = 10;
    private ObjectPool pool;

    void Awake()
    {
        pool = GameObject.FindObjectOfType<ObjectPool>();
        AddToPool();
        this.SetActive(false);
    }

    public void Pick()
    {
        GameObject.FindObjectOfType<PlayerHealth>().HealPlayer(healNum);
        pool.Release(this);
    }

    //Prototype:
    public IPrototype Clone()
    {
        return Instantiate(this).GetComponent<IPrototype>();
    }

    //Object Pool:
    public void Reset()
    {

    }

    public string GetName()
    {
        return "HealingBall";
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

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
