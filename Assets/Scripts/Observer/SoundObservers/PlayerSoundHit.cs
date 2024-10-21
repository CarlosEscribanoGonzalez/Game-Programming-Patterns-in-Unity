using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundHit: MonoBehaviour, IObserver<float>
{
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip healthSound;
    private AudioSource audioSource;
    private PlayerHealth health;
    private float currentHealth;

    void Awake()
    {
        health = this.GetComponent<PlayerHealth>();
        health.AddObserver(this);
        audioSource = this.GetComponent<AudioSource>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartObserver(float data)
    {
        if (health != null)
        {
            currentHealth = data;
        }
    }

    public void UpdateObserver(float data)
    {
        if (data < currentHealth)
        {
           audioSource.clip = hurtSound;
           audioSource?.Play();
            
        }
        else if(data > currentHealth) 
        {
  
            audioSource.clip = healthSound;
            audioSource?.Play();

        }
        currentHealth = data;


    }

}
