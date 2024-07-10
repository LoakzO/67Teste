using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{
    [Header("Values")]
    public int colorValue;

    [Header("BackRefs")]
    public PlayerScript player;
    public UIScript ui;
    public Color[] playerColor;

    Color[] unlockedColors = new Color[3];

    void Start()
    {
        UnlockColor(0);
        ChangeColor(0);
    }

    void Update()
    {
        
    }

    public void ChangeColor(int color)
    {
        player.meshMaterial.color = playerColor[color]; 

        if(playerColor[color] != unlockedColors[color])
        {
            player.money -= colorValue;
            UnlockColor(color);
        }
    }

    void UnlockColor(int index)
    {
        unlockedColors[index] = playerColor[index];
        ui.buttons[index].tag = "Unlocked";
    }
}
