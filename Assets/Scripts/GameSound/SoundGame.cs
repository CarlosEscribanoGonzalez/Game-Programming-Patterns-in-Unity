using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGame : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
