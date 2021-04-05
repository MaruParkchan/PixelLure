using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCrossEffect : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private Material crossMaterial;
    [SerializeField]
    private float fadeTime;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        crossMaterial = particleSystem.GetComponent<Renderer>().material;
    }

    public void ParticleSystemFadeOut()
    {
        StartCoroutine("FadeOut");
    }
    
    private IEnumerator FadeOut()
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = crossMaterial.color;
            color.a = Mathf.Lerp(1, 0, percent);
            crossMaterial.color = color;

            yield return null;
        }
    }
}
