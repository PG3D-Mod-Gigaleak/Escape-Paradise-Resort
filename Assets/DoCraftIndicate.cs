using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoCraftIndicate : MonoBehaviour
{
    public string[] items;

    public int[] amounts;

    void OnTriggerEnter()
    {
        if (GameObject.Find("CraftIndicatorPopup").GetComponent<CraftIndicator>().CIOpened || GameObject.Find("Inventory").GetComponent<Inventory>().inventoryOpen)
        {
            return;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        foreach(GameObject gobj in GameObject.Find("CraftIndicatorPopup").GetComponent<CraftIndicator>().PartTypes)
        {
            if (!gobj.name.StartsWith(items.Length.ToString()))
            {
                gobj.SetActive(false);
            }
            else
            {
                gobj.SetActive(true);     
                for(int i = 0; i < items.Length; i++)
                {
                    gobj.GetComponent<PartType>().icons[i].sprite = Resources.Load<GameObject>("items/" + items[i]).GetComponent<Item>().icon;
                    gobj.GetComponent<PartType>().texts[i].text = GameObject.Find("Player").GetComponent<InventoryManager>().FindAmountOfItem(items[i]) + "/" + amounts[i];
                }
            }
        }
        GameObject.Find("CraftIndicatorPopup").GetComponent<Animation>().Play("CIOpen");
        GameObject.Find("CraftIndicatorPopup").GetComponent<CraftIndicator>().CIOpened = true;
    }
}
