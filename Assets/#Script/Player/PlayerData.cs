using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private float moveSpeed; // �̵� �ӵ�
    [SerializeField]
    private float fireRate; // ���� �ӵ�
    [SerializeField]
    private Vector3 scale; // �÷��̾� ũ��

    public float MoveSpeed => moveSpeed;
    public float FireRate => fireRate;
    public Vector3 Scale => scale;
}
