using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public enum ItemType
    {
        food,
        weapon,
        other,
        none
    };

    public Item[] currentItems;

    public int[] stacks;

    public Item currentEquippedItem;

    private GameObject currentItemObj;

    private bool isEating;

    private int stackIndex;

    public void AddItem(string str, int amount)
    {
        int index = 0;
        int index2 = 0;
        Item lastItem = null;
        foreach(Item item in currentItems)
        {
            if (item.name == str && amount < item.stackSize || item.type == ItemType.none)
            {
                currentItems[index] = Resources.Load<GameObject>("items/" + str).GetComponent<Item>();
                stacks[index] += amount;
                GameObject.Find("Inventory").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("sounds/pickupitem"));
                GameObject.Find("ItemPopup_Image").GetComponent<Image>().sprite = currentItems[index].icon;
                GameObject.Find("ItemPopup_Text").GetComponent<TextMeshProUGUI>().text = "+" + amount;
                GameObject.Find("GotItemPopup").GetComponent<Animation>().Play("DoItemPopup");
                return;
            }
            index++;
        }
    }

    public void ChangeItem(int index)
    {
        Destroy(currentItemObj);
        currentEquippedItem = currentItems[index];
        currentItemObj = Instantiate(currentEquippedItem.prefab, GameObject.Find("hand").transform);
        stackIndex = index;
    }

    public void Eat()
    {
        if (currentEquippedItem.type != ItemType.food || isEating || currentItemObj.GetComponent<Animation>().IsPlaying("Eat"))
        {
            return;
        }
        currentItemObj.GetComponent<Animation>().Play("Eat");
        GetComponent<AudioSource>().PlayOneShot(currentEquippedItem.gameObject.GetComponent<Food>().EatSound);
        GetComponent<ValueController>().Hunger += currentEquippedItem.gameObject.GetComponent<Food>().HungerGive;
        stacks[stackIndex]--;
    }

    public void Shoot()
    {
    }

    public void DoOtherUse()
    {
    }

    public void DoItemUse()
    {
        if (currentEquippedItem == null || GameController.GamePaused())
        {
            return;
        }
        switch(currentEquippedItem.type)
        {
            case ItemType.food:
            Eat();
            return;

            case ItemType.weapon:
            Shoot();
            return;

            case ItemType.other:
            DoOtherUse();
            return;

            case ItemType.none:
            return;
        }
    }

        
    public int FindAmountOfItem(string itemName)
    {
        int amount = 0;
        int index = 0;
        foreach (Item theItem in currentItems)
        {
            if (theItem.name == itemName)
            {
                amount += stacks[index];
            }
            index++;
        }
        return amount;
    }

    void Update()
    {
        for (int i = 0; i < 24; i++)
        {
            if (stacks[i] <= 0 && currentItems[i].type != ItemType.none)
            {
                stacks[i] = 0;
                currentItems[i] = Resources.Load<GameObject>("items/nothing").GetComponent<Item>();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            DoItemUse();
        }
        if (Input.GetKeyDown("q") && !GameObject.Find("Inventory").GetComponent<Animation>().isPlaying)
        {
            if (!GameObject.Find("Inventory").GetComponent<Inventory>().inventoryOpen)
            {
                GameObject.Find("Inventory").GetComponent<Animation>().Play("OpenInventory");
                GameObject.Find("Inventory").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("sounds/invopen"));
                GameObject.Find("Inventory").GetComponent<Inventory>().inventoryOpen = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                GameObject.Find("Inventory").GetComponent<Animation>().Play("CloseInventory");
                GameObject.Find("Inventory").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("sounds/invclose"));
                GameObject.Find("Inventory").GetComponent<Inventory>().inventoryOpen = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
