using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBubblePattern : DogBossPatternBase
{
    [SerializeField] private GameObject bubblePrefab;

    public override IEnumerator Attacking()
    {
        int currentCount = 0;
        dogBossAnimator.SetTrigger("Bubble"); // 애니메이션 전환

        yield return new WaitForSeconds(waitTime); // 대기
        while (dogBoss.dogBossData.p1_AttackCount > currentCount)
        {
            SpawnBubble(); // 물방울 생성
            yield return new WaitForSeconds(dogBoss.dogBossData.p1_AttackDelayTime); // 생성 대기(주기)
            currentCount++;
        }
        yield return new WaitForSeconds(waitTime); // 대기
        dogBossAnimator.SetTrigger("End"); // 애니메이션 전환
    }

    private void SpawnBubble()
    {
        GameObject clone = Instantiate(bubblePrefab);
        clone.transform.position = new Vector3(Random.Range(-6.0f, 6.0f), -6.0f);
    }

    public void CoroutineStop()
    {
        StopAllCoroutines();
    }

    protected override void Init()
    {
        
    }

}
