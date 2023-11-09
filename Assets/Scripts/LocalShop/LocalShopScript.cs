using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalShopScript : MonoBehaviour
{
    public GameObject shopUI; 

    private bool isPlayerNearby = false; 
    private bool isShopOpen = false; 

    void Start()
    {
        CloseShop();
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {

            if (isShopOpen)
            {
                CloseShop();
            }
            else
            {
                OpenShop();
            }
        }
    }


    void OpenShop()
    {
        shopUI.SetActive(true);
        Time.timeScale = 0f;
        isShopOpen = true;
    }

    void CloseShop()
    {
        shopUI.SetActive(false);
        Time.timeScale = 1f; 
        isShopOpen = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            CloseShop(); 
        }
    }
}
