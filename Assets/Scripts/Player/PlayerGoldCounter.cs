using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGoldCounter : MonoBehaviour
{
    public int goldCount = 0;
    public Text goldCountText;

    private void Start()
    {
        UpdateGoldCountText();
    }

    public void AddGold(int amount)
    {
        goldCount += amount;
        UpdateGoldCountText();
    }

    public void UpdateGoldCountText()
    {
        if (goldCountText != null)
        {
            goldCountText.text = "Gold: " + goldCount;
        }
    }

}
