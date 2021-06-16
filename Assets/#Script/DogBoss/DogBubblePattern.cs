using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBubblePattern : MonoBehaviour
{
    [SerializeField] private float waitTime; // 대기 시간

    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private int spawnMaxCount; // 물방울 생성 갯수
    [SerializeField] private float spawnCycleTime; // 생성 주기 
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator IBubbleSpawner()
    {
        int currentCount = 0;
        animator.SetTrigger("Bubble"); // 애니메이션 전환

        yield return new WaitForSeconds(waitTime); // 대기
        while(spawnMaxCount > currentCount)
        {
            SpawnBubble(); // 물방울 생성
            yield return new WaitForSeconds(spawnCycleTime); // 생성 대기(주기)
            currentCount++;
        }
        yield return new WaitForSeconds(waitTime); // 대기
        animator.SetTrigger("End"); // 애니메이션 전환
    }

    private void SpawnBubble()
    {
        GameObject clone = Instantiate(bubblePrefab);
        clone.transform.position = new Vector3(Random.Range(-6.0f, 6.0f), -6.0f);
    }
}
