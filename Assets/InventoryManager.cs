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

    private Image EatBarFill;

    private GameObject EatBar;

    public void AddItemVoid(string str, int amount)
    {
        StartCoroutine(AddItem(str, amount));
    }

    void Start()
    {
        EatBar = GameObject.Find("EatBar");
        EatBarFill = GameObject.Find("EatBarFill").GetComponent<Image>();
    }

    public IEnumerator AddItem(string str, int amount)
    {
        int originalAmount = amount;
        int index = 0;
        int index2 = 0;
        Item lastItem = null;
        while (amount > 0)
        {
        yield return new WaitForSeconds(0.01f);
        Debug.Log("re-looping...");
        foreach(Item item in currentItems)
        {
            if (item.name == str)
            {
                if (Resources.Load<GameObject>("items/" + str).GetComponent<Item>().stackSize - stacks[index] == amount)
                {
                    Debug.Log("item is the same and the amount is equal to the stack size minus the current stack.");
                    stacks[index] = amount;
                    amount = 0;
                }
                if (stacks[index] == Resources.Load<GameObject>("items/" + str).GetComponent<Item>().stackSize)
                {
                    Debug.Log("continuing...");
                    continue;
                }
                if (stacks[index] < Resources.Load<GameObject>("items/" + str).GetComponent<Item>().stackSize)
                {
                    Debug.Log("item is the same and the current stak is less than the stack size.");
                    int @int = item.stackSize - stacks[index];
                    stacks[index] += amount;
                    amount -= @int;
                }
            }
            if (index == 0 && item.type == ItemType.none || item.type == ItemType.none && lastItem != null && lastItem.type != ItemType.none)
            {
                currentItems[index] = Resources.Load<GameObject>("items/" + str).GetComponent<Item>();
                if (amount <= Resources.Load<GameObject>("items/" + str).GetComponent<Item>().stackSize)
                {
                    Debug.Log("type is none and amount is equal to or less than the stack size.");
                    stacks[index] = amount;
                    amount = 0; 
                }
                if (amount > Resources.Load<GameObject>("items/" + str).GetComponent<Item>().stackSize)
                {
                    Debug.Log("type is none and amount is more than the stack size.");
                    stacks[index] = item.stackSize;
                    amount -= item.stackSize;
                }
            }
            else if (lastItem != null && lastItem.name == str)
            {
                if (Resources.Load<GameObject>("items/" + str).GetComponent<Item>().stackSize - stacks[index - 1] == amount)
                {
                    Debug.Log("item is the same and the amount is equal to the stack size minus the current stack.");
                    stacks[index - 1] = amount;
                    amount = 0;
                }
                if (stacks[index - 1] == Resources.Load<GameObject>("items/" + str).GetComponent<Item>().stackSize)
                {
                    Debug.Log("continuing...");
                    continue;
                }
                if (stacks[index - 1] < Resources.Load<GameObject>("items/" + str).GetComponent<Item>().stackSize)
                {
                    Debug.Log("item is the same and the current stak is less than the stack size.");
                    int @int = item.stackSize - stacks[index];
                    stacks[index] += amount;
                    amount -= @int;
                }
            }
            index++;
            lastItem = item;
        }
        index = 0;
        }
        GameObject.Find("Inventory").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("sounds/pickupitem"));
        GameObject.Find("ItemPopup_Image").GetComponent<Image>().sprite = Resources.Load<GameObject>("items/" + str).GetComponent<Item>().icon;
        GameObject.Find("ItemPopup_Text").GetComponent<TextMeshProUGUI>().text = "+" + originalAmount;
        GameObject.Find("GotItemPopup").GetComponent<Animation>().Play("DoItemPopup");
    }

    public void ChangeItem(int index)
    {
        Destroy(currentItemObj);
        currentEquippedItem = currentItems[index];
        currentItemObj = Instantiate(currentEquippedItem.prefab, GameObject.Find("hand").transform);
        stackIndex = index;
    }

    public IEnumerator Eat()
    {
        bool finished = false;
        float fl = 0f;
        if (currentEquippedItem.type != ItemType.food || isEating || currentItemObj.GetComponent<Animation>().IsPlaying("Eat"))
        {
            yield break;
        }
        currentItemObj.GetComponent<Animation>().Play("Eat");
        GetComponent<AudioSource>().PlayOneShot(currentEquippedItem.gameObject.GetComponent<Food>().EatSound);
        EatBar.GetComponent<Animation>().Play("FadeInEatBar");
        while (Input.GetMouseButton(0) && !finished)
        {
            yield return new WaitForSeconds(0.01f);
            fl += 0.01f * currentEquippedItem.gameObject.GetComponent<Food>().EatTimeMultiplier;
            EatBarFill.fillAmount = fl;
            if (fl >= 1f)
            {
                fl = 1f;
                finished = true;
                FinishEating();
            }
        }
        if (!finished)
        {
            currentItemObj.GetComponent<Animation>().Stop();
            currentItemObj.GetComponent<Animation>().Play("Eat");
            yield return new WaitForSeconds(0.01f);
            currentItemObj.GetComponent<Animation>().Stop();
            GetComponent<AudioSource>().Stop();
            EatBar.GetComponent<Animation>().Play("FadeOutEatBar");
        }
    }

    public void FinishEating()
    {
        if (currentEquippedItem.name == "mushroom" && !GameEvents.GetInstance().eventsDone[0])
        {
            GameEvents.GetInstance().DoGameEvent(3);
        }
        GetComponent<ValueController>().Hunger += currentEquippedItem.gameObject.GetComponent<Food>().HungerGive;
        stacks[stackIndex]--;
        currentEquippedItem = Resources.Load<GameObject>("items/nothing").GetComponent<Item>();
        Destroy(currentItemObj);
        EatBar.GetComponent<Animation>().Play("FadeOutEatBar");
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
            StartCoroutine(Eat());
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
                if (CraftMenu.GetInstance().CMOpened)
                {
                    return;
                }
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
