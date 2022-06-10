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

        if (randomIndex == 0) // ȸ�� ���� ���ǹ�
        {
            dogBoss.dogBossData.p2_RotateSpeed *= -1;
        }

        dogBossAnimator.SetTrigger("Drink");
        yield return new WaitForSeconds(waitTime);
        GameObject clone = Instantiate(dogSmallSojuCirclePrefab);
        clone.GetComponent<DogSmallSojuCircle>().Init(dogBoss.dogBossData.p2_RotateSpeed, dogBoss.dogBossData.p2_RotateTime, dogBoss.dogBossData.p2_FadeTime);
        clone.transform.position = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(waitTime + dogBoss.dogBossData.p2_RotateTime + dogBoss.dogBossData.p2_FadeTime); // ���ð� + ȸ�� �ð� + ���̵� �ð�
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
