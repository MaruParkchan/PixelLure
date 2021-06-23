using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoomPattern : MonoBehaviour 
{
    [SerializeField] private GameObject boomEffect;
    [SerializeField] private int spawnCount; // 생성할 수
    [SerializeField] private float cycleTime; // 재 생성시간
    [SerializeField] private float waitTime; // 대기 시간

    private CardBoss cardBoss;
    private Animator animator;

    private void Start()
    {
        cardBoss = GetComponent<CardBoss>();
        animator = GetComponent<Animator>();
    }

    public IEnumerator ICardBoomPattern()
    {
        Appear(); // 등장

        yield return new WaitForSeconds(waitTime);

        int currentCount = 0;

        while (currentCount <= spawnCount)
        {
            SpawnBoomEffect();
            currentCount++;
            yield return new WaitForSeconds(cycleTime);
        }

        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(waitTime); // 대기 시간
    }

    private void SpawnBoomEffect()
    {
        GameObject clone = Instantiate(boomEffect);
        clone.transform.position = new Vector3(Random.Range(cardBoss.CardBossMapData.LimitMin.x, cardBoss.CardBossMapData.LimitMax.x), 
                                               Random.Range(cardBoss.CardBossMapData.LimitMin.y, cardBoss.CardBossMapData.LimitMax.y), 0);
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
        //StopCoroutine("ICardBoomPattern");
        StopAllCoroutines();
    }
}
