using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRadialShapePattern : MonoBehaviour 
{
    [SerializeField]
    private GameObject cardRadialShapeEffect; // ����� ����Ʈ 

    [SerializeField]
    private float waitTime; // ��� �ð�
    private float attackDelayTime = 1.0f; // ���� �ӵ� (�����Ǿ���� �ʹ� ������ġ�� �ִϸ��̼� ����)
    [SerializeField]
    private int attackCount; // ���� Ƚ��

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
        Appear(); // ����
        yield return new WaitForSeconds(waitTime); // ��� �ð�
        while (attackCount > count)
        {
            animator.SetTrigger("Attack1");
            yield return new WaitForSeconds(attackDelayTime); // ���� �ӵ�
            count++; 
        }
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(waitTime); // ��� �ð�
    }

    public void SpawnCardRadialShapeEffect() // ���� ���� - 1 ���� (�����)
    {
        GameObject clone = Instantiate(cardRadialShapeEffect);
        clone.transform.position = this.transform.position;
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
       // StopCoroutine("ICardRadialShapePattern");
        StopAllCoroutines();
    }
}
