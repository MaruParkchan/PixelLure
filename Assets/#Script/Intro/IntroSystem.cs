using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroSystem : MonoBehaviour
{
    #region Cursor
    [SerializeField] private Sprite cursorLockOnSprite;
    [SerializeField] private Sprite cursorLockOffSprite;
    [SerializeField] private Button cursorButton;
    private bool isCursorLock = false;
    #endregion

    [SerializeField] private TextMeshProUGUI soundText;
    [SerializeField] private TextMeshProUGUI quitText;
    [SerializeField] private TextMeshProUGUI cursorLockText;
    [SerializeField] private TextMeshProUGUI creditText;
    [SerializeField] private TextMeshProUGUI howToText;
    [SerializeField] private TextMeshProUGUI howToMoveText;
    [SerializeField] private TextMeshProUGUI howToAttackText;

    private void Start()
    {
        //IsCursorLock();
        CursorLockInit();
        ButtnLanageChange();
    }

    public void IsCursorLock() // CursorLock OnOff
    {
        isCursorLock = !isCursorLock;

        switch (isCursorLock)
        {
            case true:
                cursorButton.image.sprite = cursorLockOnSprite;
                CursorLockSwitch(CursorLockMode.Confined);
                break;

            case false:
                cursorButton.image.sprite = cursorLockOffSprite;
                CursorLockSwitch(CursorLockMode.None);
                
                break;
        }
    }

    public void CursorLockInit()
    {
        switch (Cursor.lockState)
        {
            case CursorLockMode.Confined:
                isCursorLock = true;
                cursorButton.image.sprite = cursorLockOnSprite;
                break;
            case CursorLockMode.None:
                isCursorLock = false;
                cursorButton.image.sprite = cursorLockOffSprite;
                break;
        }

        switch (isCursorLock)
        {
            case true:
                cursorButton.image.sprite = cursorLockOnSprite;
                CursorLockSwitch(CursorLockMode.Confined);
                break;

            case false:
                cursorButton.image.sprite = cursorLockOffSprite;
                CursorLockSwitch(CursorLockMode.None);
                break;
        }
    }

    private void CursorDataImport()
    {
        //PlayerPrefs.setini
    }

    public void CursorLockSwitch(CursorLockMode cursorState)
    {
        Cursor.lockState = cursorState;
    }

    public void ButtnLanageChange()
    {
        switch (PlayerPrefs.GetInt("LanguageIndex"))
        {  
            case 0:
            soundText.text = "Sound";
            quitText.text = "Quit";
            cursorLockText.text = "CursorLock";
            creditText.text = "Credit";
            howToText.text = "HowTo";
            howToMoveText.text = "Move";
            howToAttackText.text = "Attack";
            break;

            case 1:
            soundText.text = "소리";
            quitText.text = "게임종료";
            cursorLockText.text = "마우스잠금";
            creditText.text = "정보";
            howToText.text = "게임방법";
            howToMoveText.text = "이동";
            howToAttackText.text = "공격";
            break;
        }
    }
}
