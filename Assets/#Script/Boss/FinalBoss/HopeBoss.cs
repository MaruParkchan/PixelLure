using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopeBoss : Boss
{
    [SerializeField] private MapData hopeBossMapData; // 보스 나타나는 좌표 데이터 
    public override void BossDiedEvent()
    {
    }

    protected override void ColliderEnableOff()
    {
    }

    protected override void ColliderEnableOn()
    {
    }

    protected override void CoroutineAllStop()
    {

    }

    protected override IEnumerator Phase1()
    {
       yield return new WaitForSeconds(4.0f);
       while(true)
       {


            yield return null;
       }
    }

    protected override IEnumerator Phase2()
    {
        yield return null;
    }

    protected override void SelectionEventTime()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
