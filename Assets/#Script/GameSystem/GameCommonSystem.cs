using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameCommonSystem : MonoBehaviour
{
    

    private bool isSoundOn = true;

    public static Action soundState;
  
    private void Awake()
    {
        soundState = () => { GetSoundOn(); };
    }

    public bool GetSoundOn()
    {
        return isSoundOn;
    }


    public void SoundOn()
    {
        isSoundOn = true;
    }

    public void SoundOff()
    {
        isSoundOn = false;
    }

    public void SoundOnOff(bool isSwitch)
    {
        isSoundOn = !isSwitch;
    }
}
