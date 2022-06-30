using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiedCountSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI diedCountText;

    private void Start()
    {
        diedCountText.text = "Died Count : " + PlayerPrefs.GetInt("DiedCount");
    }

    [ContextMenu("Died Count Reset")]
    public void ResetDiedCount()
    {
        PlayerPrefs.DeleteKey("DiedCount");
    }

}
