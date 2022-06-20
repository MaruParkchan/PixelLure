using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainUISystem : MonoBehaviour
{
    [Header("Stage1")]
    [SerializeField] private GameObject stage1_RedChain;
    [SerializeField] private GameObject stage1_BlueChain;
    [Header("Stage2")]
    [SerializeField] private GameObject stage2_RedChain;
    [SerializeField] private GameObject stage2_BlueChain;
    [Header("Stage3")]
    [SerializeField] private GameObject stage3_RedChain;
    [SerializeField] private GameObject stage3_BlueChain;

    [SerializeField] private int stage1_RedChainValue = 0;
    [SerializeField] private int stage1_BlueChainValue = 0;

    [SerializeField] private int stage2_RedChainValue = 0;
    [SerializeField] private int stage2_BlueChainValue = 0;

    [SerializeField] private int stage3_RedChainValue = 0;
    [SerializeField] private int stage3_BlueChainValue = 0;

    private void Start()
    {
        ChainUpdateSystem();
    }

    private void Update()
    {
        ChainValueComparison(stage1_RedChain, stage1_RedChainValue);
        ChainValueComparison(stage1_BlueChain, stage1_BlueChainValue);
        ChainValueComparison(stage2_RedChain, stage2_RedChainValue);
        ChainValueComparison(stage2_BlueChain, stage2_BlueChainValue);
        ChainValueComparison(stage3_RedChain, stage3_RedChainValue);
        ChainValueComparison(stage3_BlueChain, stage3_BlueChainValue);
    }

    private void ChainUpdateSystem()
    {
        stage1_RedChainValue = PlayerPrefs.GetInt("Stage1_RedChain");
        stage1_BlueChainValue = PlayerPrefs.GetInt("Stage1_BlueChain");
        stage2_RedChainValue = PlayerPrefs.GetInt("Stage2_RedChain");
        stage2_BlueChainValue = PlayerPrefs.GetInt("Stage2_BlueChain");
        stage3_RedChainValue = PlayerPrefs.GetInt("Stage3_RedChain");
        stage3_BlueChainValue = PlayerPrefs.GetInt("Stage3_BlueChain");

        ChainValueComparison(stage1_RedChain, stage1_RedChainValue);
        ChainValueComparison(stage1_BlueChain, stage1_BlueChainValue);
        ChainValueComparison(stage2_RedChain, stage2_RedChainValue);
        ChainValueComparison(stage2_BlueChain, stage2_BlueChainValue);
        ChainValueComparison(stage3_RedChain, stage3_RedChainValue);
        ChainValueComparison(stage3_BlueChain, stage3_BlueChainValue);
    }

    private void ChainValueComparison(GameObject clone, int Chainvalue)
    {
        if (Chainvalue == 1)
        {
            clone.SetActive(true);
        }
        else
            clone.SetActive(false);
    }

    public void AllResetChainData()
    {
        PlayerPrefs.DeleteKey("Stage1_RedChain");
        PlayerPrefs.DeleteKey("Stage1_BlueChain");
        PlayerPrefs.DeleteKey("Stage2_RedChain");
        PlayerPrefs.DeleteKey("Stage2_BlueChain");
        PlayerPrefs.DeleteKey("Stage3_RedChain");
        PlayerPrefs.DeleteKey("Stage3_BlueChain");

        stage1_RedChainValue = PlayerPrefs.GetInt("Stage1_RedChain");
        stage1_BlueChainValue = PlayerPrefs.GetInt("Stage1_BlueChain");
        stage2_RedChainValue = PlayerPrefs.GetInt("Stage2_RedChain");
        stage2_BlueChainValue = PlayerPrefs.GetInt("Stage2_BlueChain");
        stage3_RedChainValue = PlayerPrefs.GetInt("Stage3_RedChain");
        stage3_BlueChainValue = PlayerPrefs.GetInt("Stage3_BlueChain");
    }
}
