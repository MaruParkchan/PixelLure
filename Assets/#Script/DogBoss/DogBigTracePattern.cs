using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBigTracePattern : MonoBehaviour
{
    [SerializeField] private GameObject sojuObject;
    [SerializeField] private float spawnCycle; // 소주병 n초마다 스폰 
    [SerializeField] private int spawnCount; // 소주병 생성 수
    [SerializeField] private float rotateSpeed; // 회전 속도
    [SerializeField] private float moveSpeed; // 타겟 공격 이동속도
    [SerializeField] private float attackTime; // 생성후 n초 뒤 공격 
    [SerializeField] private float waitTime; // 대기시간

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
