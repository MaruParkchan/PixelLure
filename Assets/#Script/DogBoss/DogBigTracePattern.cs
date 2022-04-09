using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBigTracePattern : MonoBehaviour
{
    [SerializeField] private GameObject sojuObject;
    [SerializeField] private float spawnCycle; // ���ֺ� n�ʸ��� ���� 
    [SerializeField] private int spawnCount; // ���ֺ� ���� ��
    [SerializeField] private float rotateSpeed; // ȸ�� �ӵ�
    [SerializeField] private float moveSpeed; // Ÿ�� ���� �̵��ӵ�
    [SerializeField] private float attackTime; // ������ n�� �� ���� 
    [SerializeField] private float waitTime; // ���ð�

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator ISpawnSoju()
    {
        int currentCount = 0;
        animator.SetTrigger("Shake");
        yield return new WaitForSeconds(waitTime);
        while (currentCount < spawnCount)
        {
            SojuInit();
            currentCount++;
            yield return new WaitForSeconds(spawnCycle);
        }
        animator.SetTrigger("End");
        yield return new WaitForSeconds(waitTime);
    }

    private void SojuInit()
    {
        GameObject clone = Instantiate(sojuObject);
        clone.GetComponent<DogSojuTrace>().Init(rotateSpeed, moveSpeed, attackTime);
        clone.transform.position = new Vector3(Random.Range(-7, 7), Random.Range(-4, 4));
    }

    public void CoroutineStop()
    {
        StopAllCoroutines();
    }
}
