using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [Header("VisualRefs")]
    public Text moneyText;
    public Text BodyCountText;
    public GameObject storePanel;
    public Button levelUpButton;

    [Header("BackRefs")]
    public PlayerScript player;
    public UpgradeScript upgrade;

    void Start()
    {
        
    }

    void Update()
    {
        ApplyText();
        EnableUpgrade();
    }

    void ApplyText()
    {
        moneyText.text = player.money.ToString();
        BodyCountText.text = player.BodyCount.ToString() + "/" + player.maxBodyCount.ToString();
    }

    public void OpenStore()
    {
        if (storePanel.activeInHierarchy)
        {
            storePanel.SetActive(false);
        }
        else
        {
            storePanel.SetActive(true);
        }
    }

    void EnableUpgrade()
    {
        if (player.money >= upgrade.levelUpValue)
        {
            levelUpButton.interactable = true;
        }
        else
        {
            levelUpButton.interactable = false;
        }
    }
}
