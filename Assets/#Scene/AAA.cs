using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AAA : MonoBehaviour
{
    public static Action diglog;
    private int count = 0;

    private void Awake()
    {
        diglog = () => { Diglogs(); };
    }

    public void Diglogs()
    {
        if(count == 0)
        {
            diglog += One;
        }

        if (count == 1)
        {

        }

        if (count == 2)
        {
            diglog += Three;
        }

        Debug.Log("A");
        count++;
    }

    public void One()
    {
        Debug.Log("One");
    }

    public void Two()
    {
        Debug.Log("Two");
    }

    public void Three()
    {
        Debug.Log("Three");
    }
}
