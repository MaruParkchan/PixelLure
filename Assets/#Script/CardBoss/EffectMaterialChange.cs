using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMaterialChange : MonoBehaviour
{
    [SerializeField] private Material[] material;

    private ParticleSystemRenderer particleSystemRender;

    private void Start()
    {
        particleSystemRender = GetComponent<ParticleSystemRenderer>();
        particleSystemRender.material = material[Random.Range(0, material.Length)];
    }


}
