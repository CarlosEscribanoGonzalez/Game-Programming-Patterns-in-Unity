using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour, IObserver<float>
{
    private Slider lifeBar;
    private float maxHealth;

    void Awake()
    {
        lifeBar = GetComponent<Slider>();
        GameObject.FindObjectOfType<PlayerHealth>().AddObserver(this);
        maxHealth = GameObject.FindObjectOfType<PlayerHealth>().GetMaxHealth(); 
        lifeBar.maxValue = maxHealth;
        
    }

    void Update()
    {
        
    }
    public void StartObserver(float data)
    {
        lifeBar.value = data;
    }

    public void UpdateObserver(float data)
    {
        lifeBar.value = data;
    }

}
