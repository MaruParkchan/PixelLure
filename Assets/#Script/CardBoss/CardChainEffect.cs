using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardChainEffect : MonoBehaviour
{
    [SerializeField] private GameObject nextEffect;
    private ParticleSystem existingEffect;

    private void Start()
    {
        existingEffect = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (existingEffect.isStopped)
            SpawnNextEffect();
    }

    private void SpawnNextEffect()
    {
        GameObject clone = Instantiate(nextEffect);
        clone.transform.position = transform.position;
        Destroy(this.gameObject);
    }
}
