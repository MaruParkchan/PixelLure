using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoomPattern : MonoBehaviour 
{
    [SerializeField] private GameObject boomEffect;
    [SerializeField] private int spawnCount; // ������ ��
    [SerializeField] private float cycleTime; // �� �����ð�
    [SerializeField] private float waitTime; // ��� �ð�

    private CardBoss cardBoss;
    private Animator animator;

    private void Start()
    {
        cardBoss = GetComponent<CardBoss>();
        animator = GetComponent<Animator>();
    }

    public IEnumerator ICardBoomPattern()
    {
        Appear(); // ����

        yield return new WaitForSeconds(waitTime);

        int currentCount = 0;

        while (currentCount <= spawnCount)
        {
            SpawnBoomEffect();
            currentCount++;
            yield return new WaitForSeconds(cycleTime);
        }

        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(waitTime); // ��� �ð�
    }

    private void SpawnBoomEffect()
    {
        GameObject clone = Instantiate(boomEffect);
        clone.transform.position = new Vector3(Random.Range(cardBoss.CardBossMapData.LimitMin.x, cardBoss.CardBossMapData.LimitMax.x), 
                                               Random.Range(cardBoss.CardBossMapData.LimitMin.y, cardBoss.CardBossMapData.LimitMax.y), 0);
    }

    private void Appear()
    {
        // ���� ��ġ ����
        transform.position = new Vector3(Random.Range(cardBoss.CardBossMapData.LimitMin.x, cardBoss.CardBossMapData.LimitMax.x),
                                               Random.Range(cardBoss.CardBossMapData.LimitMin.y, cardBoss.CardBossMapData.LimitMax.y), 0);
        animator.SetTrigger("Appear"); // ���� �ִϸ��̼� ���
    }

    public void CoroutineStop()
    {
        //StopCoroutine("ICardBoomPattern");
        StopAllCoroutines();
    }
}
