using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour, ISubject<float>, ISaveableObject
{
    private readonly List<IObserver<float>> observers = new();
    private float threads = 0; 
    private bool dirty = false;
    
    void Start()
    {
        StartObservers();
    }

    public bool UseThreads(float t) //Devuelve true o false si se puede o no realizar el pago
    {
        if (threads - t < 0)
        {
            return false;
        }
        threads -= t;
        NotifyObservers();
        return true;
    }

    public void AddThreads(float t)
    {
        threads += t;
        NotifyObservers();
    }

    public float GetThreads()
    {
        return threads;
    }

    //Observer:
    public void AddObserver(IObserver<float> observer)
    {
        observers.Add(observer);
    }

    public void NotifyObservers()
    {
        dirty = true;
        foreach(IObserver<float> o in observers)
        {
            o.UpdateObserver(threads);
        }
    }

    public void RemoveObserver(IObserver<float> observer)
    {
        observers.Remove(observer);
    }

    public void StartObservers()
    {
        foreach (IObserver<float> o in observers)
        {
            o.StartObserver(threads);
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
        return new InventoryData
        {
            threadAmount = threads
        };
    }

    public void RestoreData(object data)
    {
        InventoryData iData = (InventoryData)data;
        threads = iData.threadAmount;
        dirty = true;
    }
}
