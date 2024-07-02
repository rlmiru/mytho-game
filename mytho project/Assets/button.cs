using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviour
{
public int ItemID;
public Text Price;
public GameObject ShopManager;


    void Update()
    {
        Price.text = ShopManager.GetComponent<ShopManager>().shopItems[2,ItemID].ToString();
    }
}
