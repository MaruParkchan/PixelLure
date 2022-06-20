using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBigLaserPattern : DogBossPatternBase
{
    [Header("기본 레이저 속성")]
    [SerializeField] private GameObject laserObject;// 레이저 
    [Header("추가타 레이저 속성")]
    [SerializeField] private GameObject bundleLaserObject; // 추가타 레이저
    private float bundleLaserAttackTime = 2.3f; // 애니메이션 시간과 동일해야함
    List<DogBundleLaser> bundleLaser = new List<DogBundleLaser>();

    private Vector2 limitPosition;
    private Quaternion euler;

    private float xPos = 8.3f;
    private float ypos = 4.4f;
    // -8.3f / 8.3f -- x값 z = 90

    // -4.4f / 4.4f -- y값 z = 0

    protected override void Init()
    {

    }

    public override IEnumerator Attacking()
    {
        CameraRotate();
        int currentCount = 0;
        yield return new WaitForSeconds(waitTime);
        dogBossAnimator.SetTrigger("Posing");
        while (currentCount < dogBoss.dogBossData.p4_AttackCount)
        {
            SpawnLaserInitAndSpawn(laserObject);
            yield return new WaitForSeconds(dogBoss.dogBossData.p4_AttackDelayTime);
            currentCount++;
        }

        if (dogBoss.dogBossData.p4_IsArousal) // 각성중인가? 추가타를 하는가?
        {
            dogBossAnimator.SetTrigger("AddAttack");
            yield return new WaitForSeconds(bundleLaserAttackTime);
        }

        dogBossAnimator.SetTrigger("End");
        yield return new WaitForSeconds(waitTime);
    }


    private void SpawnLaserInitAndSpawn(GameObject laserObject)
    {
        int index = Random.Range(0, 2);

        if (index == 0)
        {
            limitPosition = new Vector2(Random.Range(-xPos, xPos), 0);
            euler = Quaternion.Euler(0, 0, 90.0f);
            LaserSpawn(laserObject, limitPosition, euler);
        }
        else
        {
            limitPosition = new Vector2(0, Random.Range(-ypos, ypos));
            euler = Quaternion.Euler(0, 0, 0);
            LaserSpawn(laserObject, limitPosition, euler);
        }
    }

    private void LaserSpawn(GameObject laserObject, Vector3 pos, Quaternion euler)
    {
        GameObject clone = Instantiate(laserObject);
        clone.transform.position = pos;
        clone.transform.rotation = euler;

        if (clone.GetComponent<DogBundleLaser>() != null)
        {
            clone.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));
            bundleLaser.Add(clone.GetComponent<DogBundleLaser>());
        }
    }

    public void AdditionalHits() // 추가타 공격
    {
        SpawnLaserInitAndSpawn(bundleLaserObject);
        SpawnLaserInitAndSpawn(bundleLaserObject);
    }

    public void AdditionalHitsBomb() // 추가타 폭파
    {
        for (int i = 0; i < bundleLaser.Count; i++) // 추가타 공격
        {
            bundleLaser[i].Attack();
        }

        bundleLaser.Clear();
    }

    public void CoroutineStop()
    {
        StopAllCoroutines();
    }
}
