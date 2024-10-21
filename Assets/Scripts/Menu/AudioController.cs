using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioController : MonoBehaviour, ISaveableOption
{
    [SerializeField] private Slider slider;
    AudioSource[] audioSources;

    private void OnLevelWasLoaded(int level)
    {
        ChangeAudioLevel();
    }

    public void ChangeAudioLevel()
    {
        audioSources = GameObject.FindObjectsOfType<AudioSource>();
        foreach(AudioSource source in audioSources)
        {
            source.volume = slider.value;
        }
    }

    public object GetData()
    {
        return new OptionsData
        {
            value = slider.value
        };
    }

    public void RestoreData(object data)
    {
        OptionsData oData = (OptionsData)data;
        slider.value = oData.value;
        ChangeAudioLevel();
    }

}
