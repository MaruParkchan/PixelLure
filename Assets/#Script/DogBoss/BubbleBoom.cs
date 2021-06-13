using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBoom : MonoBehaviour
{
    private ParticleSystem particleSystem;
    [SerializeField] private float speed;


    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0.0f, 30.0f) });
    }
}
