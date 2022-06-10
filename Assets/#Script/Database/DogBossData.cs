using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DogBossData : ScriptableObject
{

    [Header("물방울 패턴1")]
    [Range(1, 20)] public int p1_AttackCount; // 패턴1 공격 횟수
    [Range(0.01f, 3.00f)] public float p1_AttackDelayTime; // 패턴1 공격 시간
    [Header("소주병풍차 패턴2")]
    [Range(30.00f, 300.00f)] public float p2_RotateSpeed;  // 패턴2 소주병 회전 속도
    [Range(5.00f, 20.00f)] public float p2_RotateTime;  // 패턴2 소주병 나타날 시간
    [Range(0.01f, 10.00f)] public float p2_FadeTime; // 패턴2 소주병 페이드 걸리는 시간

    [Header("소주병 추적 패턴3")]
    [Range(1, 100)] public int p3_AttackCount; // 패턴3  공격 횟수
    [Range(0.01f, 3.00f)] public float p3_AttackDelayTime; // 패턴3  공격 딜레이
    [Range(10.0f, 720.0f)] public float p3_RotateSpeed; // 소주병 회전 속도
    [Range(1.0f, 100.0f)] public float p3_TargetTraceSpeed; // 타겟한테 향하는 속도
    [Range(0.1f, 3.0f)] public float p3_AttackWaitTime; // 소주병 생성 후 n초 후 공격
    [Header("레이저 패턴4")]
    [Range(1, 100)] public int p4_AttackCount; // 패턴4  공격 횟수
    [Range(0.01f, 2.00f)] public float p4_AttackDelayTime; // 패턴4  공격 딜레이
    public bool p4_IsArousal;
    [Header("책상 내려치기 패턴5")]
    [Range(1,10)] public int p5_AttackCount;
    [Range(0.01f, 2.00f)] public int p5_AttackDelayTime;
    //[Range()]
  //  [Header("소주병 터치기 패턴5")]
  //  [Range(1, 100)] public int p5_AttackCount; // 패턴4  공격 횟수
  //  [Range(0.01f, 2.00f)] public float p5_AttackDelayTime; // 패턴4  공격 딜레이

    public bool isP1;
    public bool isP2;
    public bool isP3;
    public bool isP4;
}
