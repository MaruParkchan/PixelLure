using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioSourceControl : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;
    private void Start()
    {
        Setting();
    }

    public void Setting()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            for (int i = 0; i < audioSources.Length; i++)
                audioSources[i].mute = true;
        }
        else
        {
            for (int i = 0; i < audioSources.Length; i++)
                audioSources[i].mute = false;
        }
    }
}
