using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBubbleBoomEffect : MonoBehaviour
{
    private ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void ParticleSystemInit(float countValue)
    {
        particleSystem.emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0.0f, countValue) });
    }
}
