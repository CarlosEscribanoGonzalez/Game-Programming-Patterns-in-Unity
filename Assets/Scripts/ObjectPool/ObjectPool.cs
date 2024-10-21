using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour, IObjectPool
{
    private List<IPooleableObject> pool = new List<IPooleableObject>();
    public void Add(IPooleableObject obj) //Como el pool será de varios objetos estos se añaden desde sus scripts (IPooleableObject)
    {
        pool.Add(obj);
    }

    public IPooleableObject Get(string name)
    {
       
        foreach(IPooleableObject obj in pool)
        {
            if (obj.GetName() == name && !obj.IsActive())
            {
                obj.SetActive(true);
                return obj;
            } 
        }
        return null;
    }

    public IPooleableObject Get(string name, Vector3 position)
    {
        foreach (IPooleableObject obj in pool)
        {
            if (obj.GetName() == name && !obj.IsActive())
            {
                obj.SetActive(true);
                obj.GetGameObject().transform.position = position;
                return obj;
            }
        }
        return null;
    }



    public void Release(IPooleableObject obj)
    {
        obj.SetActive(false);
        obj.Reset();
    }
}
