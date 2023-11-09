using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    public int[,] shopItems = new int[4, 4];

    private PlayerGoldCounter playerGoldCounter;
    private PlayerHealth playerHealth;
    private PlayerAttack playerAttack;
    private PlayerMovement playerMovement;

    void Start()
    {
        //ID
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;

        //Price
        shopItems[2, 1] = 5;
        shopItems[2, 2] = 4;
        shopItems[2, 3] = 6;

        // Initialize playerGoldCounter
        playerGoldCounter = FindObjectOfType<PlayerGoldCounter>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerAttack = FindObjectOfType<PlayerAttack>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void ApplyBuff(int itemID)
    {
        switch (itemID)
        {
            case 1: // HP Buff
                playerHealth.IncreaseHealth(1); 
                break;
            case 2: // Speed Buff
                playerMovement.IncreaseSpeed(1.0f); 
                break;
            case 3: // Attack Buff
                playerAttack.IncreaseAttack(1);
                break;
            default:
                break;
        }
    }

    public void Buy()
    {
        GameObject ButtonRef = EventSystem.current.currentSelectedGameObject;

        if (playerGoldCounter.goldCount >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            playerGoldCounter.goldCount -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            playerGoldCounter.UpdateGoldCountText();
            ApplyBuff(ButtonRef.GetComponent<ButtonInfo>().ItemID); 
        }
    }
}
