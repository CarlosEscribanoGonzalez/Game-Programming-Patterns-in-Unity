using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound: MonoBehaviour, IObserver<float>
{
    [SerializeField] private AudioClip hurtSoundRobot;
    [SerializeField]private AudioClip hurtSoundThingy;
    private AudioSource audioSource;
    private EnemyHealth enemy;
    private string enemyType;

    void Awake()
    {
        enemy = this.GetComponent<EnemyHealth>();
        enemy.AddObserver(this);
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
        enemyType = this.GetComponent<EnemyController>().GetName();
    }

    public void UpdateObserver(float data)
    {
        if (enemyType == "Robot")
        {
            audioSource.clip = hurtSoundRobot;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = hurtSoundThingy;
            audioSource.Play();
        }
    }
}

 



