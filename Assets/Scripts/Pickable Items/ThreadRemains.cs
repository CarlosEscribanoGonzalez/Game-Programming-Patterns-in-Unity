using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadRemains : MonoBehaviour, IPickable, IPooleableObject, IPrototype
{
    [SerializeField] private float threadNum = 10;
    private ObjectPool pool;

    void Awake()
    {
        pool = GameObject.FindObjectOfType<ObjectPool>();
        AddToPool();
        this.SetActive(false);
    }

    public void Pick()
    {
        GameObject.FindObjectOfType<InventoryManager>().AddThreads(threadNum);
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
        return "ThreadRemains";
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
