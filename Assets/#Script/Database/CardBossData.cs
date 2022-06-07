using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardBossData : ScriptableObject
{
    [Header("카드 패턴1")]
    public int p1_attackCount; // 패턴1 공격 횟수
    [Header("카드 패턴2")]
    public int p2_attackCount;  // 패턴2 공격 횟수
    public float p2_attackDelayTime;  // 패턴2 공격 딜레이
    public float p2_cardColorChangeTime; // 패턴2 카드 변화속도
    public float p2_cardMoveSpeed; // 패턴2  카드 속도
    [Header("카드 패턴3")]
    public int p3_attackCount; // 패턴3  공격 횟수
    public float p3_attackDelayTime; // 패턴3  공격 딜레이
    public float p3_accelerationWaitTime; // 킹카드 가속도 대기 시간
    public float p3_initialMoveSpeed;     // 킹카드 처음 이동속도
    public float p3_accelerationSpeed;    // 킹카드 가속도
    [Header("카드 패턴4")]
    public int p4_attackCount; // 패턴4  공격 횟수
    public float p4_attackDelayTime; // 패턴4  공격 딜레이
}
