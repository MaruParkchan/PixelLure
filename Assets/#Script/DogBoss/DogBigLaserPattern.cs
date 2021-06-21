using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBigLaserPattern : MonoBehaviour
{
    [Header("기본 레이저 속성")]
    [SerializeField] private GameObject laserObject;// 레이저 
    [SerializeField] private float laserSpawnCycleTime; // 재 생성 시간
    [SerializeField] private int laserSpawnMaxCount; // 레이저 생성수 
    [Header("추가타 레이저 속성")]
    [SerializeField] private GameObject bundleLaserObject; // 추가타 레이저
    [SerializeField] private float bundleLaserAttackTime; // 추가타 총 공격시간
                                                          //  [SerializeField] private int bundleLaserSpawnMaxCount; // 추가타 레이저 생성수 
    [SerializeField] private DogBundleLaser[] dogBundleLasers = new DogBundleLaser[8];
    List<DogBundleLaser> bundleLaser = new List<DogBundleLaser>();
    private int bunlaserIndex = 0;

    [SerializeField] private float waitTime;
    private Animator animator;
    private Vector2 limitPosition;
    private Quaternion euler;
    [SerializeField]
    private bool isArousal; // 각성상태인가? ( 추가타 )

    private float xPos = 8.3f;
    private float ypos = 4.4f;
    // -8.3f / 8.3f -- x값 z = 90

    // -4.4f / 4.4f -- y값 z = 0

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator ILaserPattern()
    {
        int currentCount = 0;
        yield return new WaitForSeconds(waitTime);
        animator.SetTrigger("Posing");
        while (laserSpawnMaxCount > currentCount)
        {
            SpawnLaserInitAndSpawn(laserObject);
            yield return new WaitForSeconds(laserSpawnCycleTime);
            currentCount++;
        }

        currentCount = 0;

        if (isArousal) // 각성중인가? 추가타를 하는가?
        {
            animator.SetTrigger("AddAttack");
            //while (bundleLaserSpawnMaxCount > currentCount)
            //{
            //    yield return new WaitForSeconds(bundleLaserSpawnCycleTime);
            //    SpawnLaserInitAndSpawn(bundleLaserObject);
            //    currentCount++;
            //}

            //for(int i = 0; i < dogBundleLasers.Length; i++) // 추가타 공격
            //{
            //    dogBundleLasers[i].Attack();
            //}
            //bunlaserIndex = 0;
            yield return new WaitForSeconds(bundleLaserAttackTime);
        }

        animator.SetTrigger("End");
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
        //    clone.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));
            bundleLaser.Add(clone.GetComponent<DogBundleLaser>());
            //dogBundleLasers[bunlaserIndex] = clone.GetComponent<DogBundleLaser>();
            //bunlaserIndex += 1;
        }
    }

    public void AdditionalHits() // 추가타 공격
    {
        SpawnLaserInitAndSpawn(bundleLaserObject);
    }

    public void AdditionalHitsBomb() // 추가타 폭파
    {
        for (int i = 0; i < bundleLaser.Count; i++) // 추가타 공격
        {
            bundleLaser[i].Attack();
            Debug.Log("i = " + i);
        }

        bundleLaser.Clear();
   //     bunlaserIndex = 0;
    }

    //private void BundleLaserSpawn(Vector3 pos, Quaternion euler)
    //{
    //    GameObject clone = Instantiate(laserObject);
    //    clone.transform.position = pos;
    //    clone.transform.rotation = euler;

    //    if(clone.GetComponent<DogBundleLaser>() != null)
    //    {
    //        dogBundleLasers[bunlaserIndex] = clone.GetComponent<DogBundleLaser>();
    //        bunlaserIndex++;
    //    }
    //}
}
