using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGoldCounter : MonoBehaviour
{
    public int goldCount = 0; // Початкова кількість "монет" гравця
    public Text goldCountText; // Текст для відображення кількості "монет" на екрані

    private void Start()
    {
        UpdateGoldCountText();
    }
    
    public void AddGold(int amount)
    {
        goldCount += amount;
        UpdateGoldCountText();
    }

    private void UpdateGoldCountText()
    {
        if (goldCountText != null)
        {
            goldCountText.text = "Gold: " + goldCount;
        }
    }
}
