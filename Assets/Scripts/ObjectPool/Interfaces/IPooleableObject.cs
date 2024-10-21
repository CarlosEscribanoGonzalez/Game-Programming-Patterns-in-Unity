using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooleableObject
{
    public void Reset();
    public string GetName();
    public void SetActive(bool active);
    public bool IsActive();
    public void AddToPool();
    public GameObject GetGameObject();
}
