using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardBossData : ScriptableObject
{
    [Header("ī�� ����1")]
    [Range(1, 20)] public int p1_attackCount; // ����1 ���� Ƚ��
    [Header("ī�� ����2")]
    [Range(1, 300)]
    public int p2_attackCount;  // ����2 ���� Ƚ��
    [Range(0.01f, 3.00f)]
    public float p2_attackDelayTime;  // ����2 ���� ������
    [Range(0.01f, 3.00f)]
    public float p2_cardColorChangeTime; // ����2 ī�� ��ȭ�ӵ�
    [Range(1.0f, 10.0f)]
    public float p2_cardMoveSpeed; // ����2  ī�� �ӵ�
    [Header("ī�� ����3")]
    [Range(1, 100)]
    public int p3_attackCount; // ����3  ���� Ƚ��
    [Range(0.01f, 3.00f)]
    public float p3_attackDelayTime; // ����3  ���� ������

    public float p3_accelerationWaitTime; // ŷī�� ���ӵ� ��� �ð�
    public float p3_initialMoveSpeed;     // ŷī�� ó�� �̵��ӵ�
    public float p3_accelerationSpeed;    // ŷī�� ���ӵ�
    [Header("ī�� ����4")]
    [Range(1, 100)]
    public int p4_attackCount; // ����4  ���� Ƚ��
     [Range(0.01f, 3.00f)]
    public float p4_attackDelayTime; // ����4  ���� ������
}
