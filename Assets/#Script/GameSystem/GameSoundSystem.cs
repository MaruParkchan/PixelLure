using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameSoundSystem : MonoBehaviour
{
    [SerializeField] private Sprite soundOnImage;
    [SerializeField] private Sprite soundOffImage;
    [SerializeField] private Button soundControlButton;
    [SerializeField] private GameAudioSourceControl gameAudioSourceControl;
    [SerializeField] private bool isSoundOn = false;
    
    private void Awake()
    {
        SoundSetting();
    }

    public void SoundOn()
    {
        isSoundOn = true;
    }

    public void SoundOff()
    {
        isSoundOn = false;
    }

    

    public void SoundOnOff()
    {
        isSoundOn = !isSoundOn;

        if (isSoundOn == true)
        {
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        SoundSetting();
        gameAudioSourceControl.Setting();
    }

    private void SoundSetting()
    {
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            soundControlButton.image.sprite = soundOnImage;
            isSoundOn = true;
        }
        else
        {
            soundControlButton.image.sprite = soundOffImage;
            isSoundOn = false;
        }
    }
}
