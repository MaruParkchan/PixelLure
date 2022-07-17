using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : MonoBehaviour
{
    [Range(0.00f, 1.00f)]
    [SerializeField] private float timescaleValue;

    void Update()
    {
        Time.timeScale = timescaleValue;
    }
}
