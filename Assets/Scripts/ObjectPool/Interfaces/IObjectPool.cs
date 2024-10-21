using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPool
{
    public IPooleableObject Get(string name);
    public IPooleableObject Get(string name, Vector3 position);
    public void Release(IPooleableObject obj);
    public void Add(IPooleableObject obj);
}
