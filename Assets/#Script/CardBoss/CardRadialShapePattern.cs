using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRadialShapePattern : MonoBehaviour 
{
    [SerializeField]
    private GameObject cardRadialShapeEffect; // 방사형 이펙트 

    [SerializeField]
    private float waitTime; // 대기 시간
    private float attackDelayTime = 1.0f; // 공격 속도 (고정되어야함 너무 낮은수치면 애니메이션 씹힘)
    [SerializeField]
    private int attackCount; // 공격 횟수

    private CardBoss cardBoss;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        cardBoss = GetComponent<CardBoss>();
    }

    public IEnumerator ICardRadialShapePattern()
    {
        int count = 0;
        Appear(); // 등장
        yield return new WaitForSeconds(waitTime); // 대기 시간
        while (attackCount > count)
        {
            animator.SetTrigger("Attack1");
            yield return new WaitForSeconds(attackDelayTime); // 공격 속도
            count++; 
        }
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(waitTime); // 대기 시간
    }

    public void SpawnCardRadialShapeEffect() // 패턴 공격 - 1 생성 (방사형)
    {
        GameObject clone = Instantiate(cardRadialShapeEffect);
        clone.transform.position = this.transform.position;
    }

    private void Appear()
    {
        // 랜덤 위치 등장
        transform.position = new Vector3(Random.Range(cardBoss.CardBossMapData.LimitMin.x, cardBoss.CardBossMapData.LimitMax.x),
                                               Random.Range(cardBoss.CardBossMapData.LimitMin.y, cardBoss.CardBossMapData.LimitMax.y), 0);
        animator.SetTrigger("Appear"); // 등장 애니메이션 재생
    }

    public void CoroutineStop()
    {
       // StopCoroutine("ICardRadialShapePattern");
        StopAllCoroutines();
    }
}
