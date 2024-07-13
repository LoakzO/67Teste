using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [Header("VisualRefs")]
    public Text moneyText;
    public Text BodyCountText;
    public Text valueText;
    public GameObject storePanel;
    public Button levelUpButton;

    [Header("Sprite Change")]
    public Image storeSprite;
    public Sprite[] sprites;

    [Header("Spacial UI")]
    public GameObject arrow;

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
        EnableArrow();
        ChangeStoreSprite();
    }

    void ApplyText()
    {
        moneyText.text = player.money.ToString();
        BodyCountText.text = player.BodyCount.ToString() + "/" + player.maxBodyCount.ToString();
        valueText.text = upgrade.levelUpValue.ToString();
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
    
    void EnableArrow()
    {
        if (player.BodyCount > 0)
        {
            arrow.SetActive(true);
        }
        else
        {
            arrow.SetActive(false);
        }
    }

    void ChangeStoreSprite()
    {
        if (storePanel.activeInHierarchy)
        {
            storeSprite.sprite = sprites[1];
        }
        else
        {
            storeSprite.sprite = sprites[0];
        }
    }
}
