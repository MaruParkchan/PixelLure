using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBubblePattern : MonoBehaviour
{
    [SerializeField] private float waitTime; // ��� �ð�

    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private int spawnMaxCount; // ����� ���� ����
    [SerializeField] private float spawnCycleTime; // ���� �ֱ� 
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator IBubbleSpawner()
    {
        int currentCount = 0;
        animator.SetTrigger("Bubble"); // �ִϸ��̼� ��ȯ

        yield return new WaitForSeconds(waitTime); // ���
        while(spawnMaxCount > currentCount)
        {
            SpawnBubble(); // ����� ����
            yield return new WaitForSeconds(spawnCycleTime); // ���� ���(�ֱ�)
            currentCount++;
        }
        yield return new WaitForSeconds(waitTime); // ���
        animator.SetTrigger("End"); // �ִϸ��̼� ��ȯ
    }

    private void SpawnBubble()
    {
        GameObject clone = Instantiate(bubblePrefab);
        clone.transform.position = new Vector3(Random.Range(-6.0f, 6.0f), -6.0f);
    }
}
