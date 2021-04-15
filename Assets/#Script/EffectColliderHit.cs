using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectColliderHit : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.CompareTag("Player"))
        {
            // PLAYER HIT 코드넣기
        }
    }
}
