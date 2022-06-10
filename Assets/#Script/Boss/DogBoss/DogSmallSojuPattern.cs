using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSmallSojuPattern : DogBossPatternBase
{
    [SerializeField]
    private GameObject dogSmallSojuCirclePrefab;

    public override IEnumerator Attacking()
    {
        int randomIndex = Random.Range(0, 2); // 0 = false , 1 = true

        if (randomIndex == 0) // 회전 방향 조건문
        {
            dogBoss.dogBossData.p2_RotateSpeed *= -1;
        }

        dogBossAnimator.SetTrigger("Drink");
        yield return new WaitForSeconds(waitTime);
        GameObject clone = Instantiate(dogSmallSojuCirclePrefab);
        clone.GetComponent<DogSmallSojuCircle>().Init(dogBoss.dogBossData.p2_RotateSpeed, dogBoss.dogBossData.p2_RotateTime, dogBoss.dogBossData.p2_FadeTime);
        clone.transform.position = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(waitTime + dogBoss.dogBossData.p2_RotateTime + dogBoss.dogBossData.p2_FadeTime); // 대기시간 + 회전 시간 + 페이드 시간
        dogBossAnimator.SetTrigger("End");
    }

    public void CoroutineStop()
    {
        StopAllCoroutines();
    }

    protected override void Init()
    {
       
    }

}
