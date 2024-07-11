using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{
    [Header("Values")]
    public int levelUpValue;

    [Header("BackRefs")]
    public PlayerScript player;
    public Color[] playerColor;

    void Start()
    {
        ChangeColor(0);
    }

    void Update()
    {
        
    }

    public void ChangeColor(int color)
    {
        player.meshMaterial.color = playerColor[color]; 
    }

    public void LevelUp()
    {
        player.maxBodyCount++;
        player.money -= levelUpValue;
        player.level++;
        ChangeColor(player.level - 1);
        levelUpValue *= 2;
    }
}
