using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextUpdate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentText;
    [SerializeField] private string datas = "�׽�Ʈ�� ���� ���Դϴ�. ���������� Text�� ������Ʈ�� �Ұ��Դϴ�.";
    [SerializeField] private float cycleTime;

    private void Start()
    {
        currentText.text = "";
        StartCoroutine("TextUp");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine("TextUp");
        }
    }

    IEnumerator TextUp()
    {
        Debug.Log("AA");

        for(int i = 0; i < datas.Length; i++)
        {
            currentText.text += datas[i];
            yield return new WaitForSeconds(cycleTime);
        }

        Debug.Log("End");
    }
}
