using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [Header("VisualRefs")]
    public Text moneyText;
    public GameObject storePanel;
    public Button[] buttons;

    [Header("BackRefs")]
    public PlayerScript player;
    public UpgradeScript upgrade;

    void Start()
    {
        
    }

    void Update()
    {
        ApplyText();
        EnableUpgrade(0);
        EnableUpgrade(1);
        EnableUpgrade(2);
    }

    void ApplyText()
    {
        moneyText.text = player.money.ToString();
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

    void EnableUpgrade(int index)
    {
        if(player.money >= upgrade.colorValue || buttons[index].tag == "Unlocked")
        {
            buttons[index].interactable = true;
            //buttons[1].interactable = true;
        }
        else
        {
            buttons[index].interactable = false;
            //buttons[1].interactable = false;
        }
    }
}
