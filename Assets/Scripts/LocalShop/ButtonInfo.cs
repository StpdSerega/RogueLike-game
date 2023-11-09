using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public Text PriceTxt;
    public ShopManager shopManager; 

    void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();
    }

    public void OnButtonClicked()
    {
        shopManager.Buy();
    }

    void Update()
    {
        PriceTxt.text = "Price: " + shopManager.shopItems[2, ItemID] + " G";
    }
}
