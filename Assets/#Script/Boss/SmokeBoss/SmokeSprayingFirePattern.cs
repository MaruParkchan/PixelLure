using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeSprayingFirePattern : MonoBehaviour
{
    [SerializeField] private MapData smokeBossMapData;
    [SerializeField] private GameObject SprayingFirePrefab;
    [SerializeField] private int fireCount; // °ø°Ý È½¼ö
    [SerializeField] private float attackCycleTime;
    [SerializeField] private float waitTime;
    private Animator animator;
    private SmokeBoss smokeBoss;

    private void Start()
    {
        animator = GetComponent<Animator>();
        smokeBoss = GetComponent<SmokeBoss>();
    }

    public IEnumerator SprayingFire()
    {
        int currentCount = 0;
        yield return new WaitForSeconds(waitTime);
        Debug.Log("B");
        while (fireCount > currentCount)
        {
           // smokeBoss.HideorAppear();
            Appear();
            yield return new WaitForSeconds(attackCycleTime);
            currentCount++;
        }
    }

    private void Appear()
    {
        transform.position = new Vector3(Random.Range(smokeBossMapData.LimitMin.x, smokeBossMapData.LimitMax.x), 
                                         Random.Range(smokeBossMapData.LimitMin.y, smokeBossMapData.LimitMax.y));

        smokeBoss.HideorAppear();
        animator.SetTrigger("Ready");
    }

    public void EngrgyBoom()
    {
        GameObject clone = Instantiate(SprayingFirePrefab);
        clone.transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y + 0.5f);
        smokeBoss.HideorAppear();
        animator.SetTrigger("Hide");
    }

    public void CoroutineStop()
    {
        //StopCoroutine("ICardKingCardPattern");
        StopAllCoroutines();
    }

}
