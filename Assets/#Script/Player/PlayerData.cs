using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private float moveSpeed; // 이동 속도
    [SerializeField]
    private float fireRate; // 공격 속도
    [SerializeField]
    private Vector3 scale; // 플레이어 크기

    public float MoveSpeed => moveSpeed;
    public float FireRate => fireRate;
    public Vector3 Scale => scale;
}
