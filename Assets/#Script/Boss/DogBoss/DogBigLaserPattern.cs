using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBigLaserPattern : DogBossPatternBase
{
    [Header("�⺻ ������ �Ӽ�")]
    [SerializeField] private GameObject laserObject;// ������ 
    [Header("�߰�Ÿ ������ �Ӽ�")]
    [SerializeField] private GameObject bundleLaserObject; // �߰�Ÿ ������
    private float bundleLaserAttackTime = 2.3f; // �ִϸ��̼� �ð��� �����ؾ���
    List<DogBundleLaser> bundleLaser = new List<DogBundleLaser>();

    private Vector2 limitPosition;
    private Quaternion euler;

    private float xPos = 8.3f;
    private float ypos = 4.4f;
    // -8.3f / 8.3f -- x�� z = 90

    // -4.4f / 4.4f -- y�� z = 0

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

        if (dogBoss.dogBossData.p4_IsArousal) // �������ΰ�? �߰�Ÿ�� �ϴ°�?
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

    public void AdditionalHits() // �߰�Ÿ ����
    {
        SpawnLaserInitAndSpawn(bundleLaserObject);
        SpawnLaserInitAndSpawn(bundleLaserObject);
    }

    public void AdditionalHitsBomb() // �߰�Ÿ ����
    {
        for (int i = 0; i < bundleLaser.Count; i++) // �߰�Ÿ ����
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
