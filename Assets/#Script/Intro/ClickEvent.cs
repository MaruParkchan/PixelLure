using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour
{
    // sprite�� Ŭ���ý����� ���� ��ũ��Ʈ 
    [SerializeField]
    private MapData mapData;
    
    [SerializeField]
    private BoxCollider2D collider;
    [SerializeField]
    private GameObject titleNameObject; // Ÿ��Ʋ 
    [SerializeField]
    private GameObject stage1Sprite; // ����1 �̹���
    [SerializeField]
    private GameObject stage2Sprite; // ����2 �̹���
    [SerializeField]
    private GameObject stage3Sprite; // ����3 �̹���
    [SerializeField]
    private IntroCrossEffect introCrossEffect; // ��Ʈ�� ����Ʈ

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine("Click");
        }
      //  LimitPosition();
    }

    IEnumerator Click()
    {
        collider.enabled = true;
        yield return new WaitForSeconds(0.05f);
        collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Stage1"))
        {
            collision.GetComponent<IntroButtonEffect>().StartTwinkle();
            collision.GetComponent<SceneLoader>().StartLoadScene("Stage1");
            this.gameObject.SetActive(false);
            titleNameObject.SetActive(false);
            stage2Sprite.SetActive(false);
            stage3Sprite.SetActive(false);
            introCrossEffect.ParticleSystemFadeOut();
        }
        else if (collision.transform.CompareTag("Stage2"))
        {
            collision.GetComponent<IntroButtonEffect>().StartTwinkle();
            collision.GetComponent<SceneLoader>().StartLoadScene("Stage2");
            this.gameObject.SetActive(false);
            titleNameObject.SetActive(false);
            stage1Sprite.SetActive(false);
            stage3Sprite.SetActive(false);
            introCrossEffect.ParticleSystemFadeOut();
        }
        else if (collision.transform.CompareTag("Stage3"))
        {
            collision.GetComponent<IntroButtonEffect>().StartTwinkle();
            collision.GetComponent<SceneLoader>().StartLoadScene("Stage3");
            this.gameObject.SetActive(false);
            titleNameObject.SetActive(false);
            stage1Sprite.SetActive(false);
            stage2Sprite.SetActive(false);
            introCrossEffect.ParticleSystemFadeOut();
        }
    }

    private void LimitPosition() // �̵����� ���콺 ������
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, mapData.LimitMin.x, mapData.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, mapData.LimitMin.y, mapData.LimitMax.y));
    }
}
