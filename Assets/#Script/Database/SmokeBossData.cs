using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SmokeBossData : ScriptableObject
{
    [Header("라이터 뿌리는 패턴1")]
    [Range(1, 30)] public int p1_AttackCount; // count 만큼 공격
    [Range(0.01f, 3.00f)] public float p1_MoveTime; // 해당 지점까지 도착시간
    [Range(1,15)] public int p1_TouchDropCount; // 라이터뿌리는 수
    [Range(0.01f, 2.00f)] public float p1_DropDelayTime; // 뿌리는 시간 간격

    [Header("미니담배 패턴2")]
    [Range(1, 100)] public int p2_SpawnCount;  // 미니담배 스폰 수
    [Range(0.01f, 2.00f)] public float p2_RespawnCycleTime; // 담배 재 생성시간

    [Header("재떨이 낙하 패턴3")]
    [Range(1, 20)] public int p3_SpawnCount;  // 재떨이 스폰 수
    [Range(0.01f, 2.00f)] public float p3_RespawnCycleTime; // 재떨이 재 생성시간

    [Header("불꽃 터트리기 패턴4")]
    [Range(1, 20)] public int p4_AttackCount;  // 불꽃 스폰 수
    [Range(1, 10)] public int p4_FireBulletCount; // 한번에 불꽃 생성 수 
    [Range(0.01f, 2.00f)] public float p4_RespawnCycleTime; //  재 공격시간


    public bool isP1;
    public bool isP2;
    public bool isP3;
    public bool isP4;
}