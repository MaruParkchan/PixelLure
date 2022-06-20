using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBigTracePattern : DogBossPatternBase
{
    [SerializeField] private GameObject sojuObject;

    public override IEnumerator Attacking()
    {
        CameraRotate();
        int currentCount = 0;
        dogBossAnimator.SetTrigger("Shake");
        yield return new WaitForSeconds(waitTime);
        while (currentCount < dogBoss.dogBossData.p3_AttackCount)
        {
            SojuInit();
            currentCount++;
            yield return new WaitForSeconds(dogBoss.dogBossData.p3_AttackDelayTime);
        }
        dogBossAnimator.SetTrigger("End");
        yield return new WaitForSeconds(waitTime);
    }

    private void SojuInit()
    {
        GameObject clone = Instantiate(sojuObject);
        clone.GetComponent<DogSojuTrace>().Init(dogBoss.dogBossData.p3_RotateSpeed, dogBoss.dogBossData.p3_TargetTraceSpeed, dogBoss.dogBossData.p3_AttackWaitTime);
        clone.transform.position = new Vector3(Random.Range(-7, 7), Random.Range(-4, 4));
    }

    public void CoroutineStop()
    {
        StopAllCoroutines();
    }

    protected override void Init()
    {
        
    }

}
