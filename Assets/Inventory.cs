using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{

    public Image[] images;

    public TextMeshProUGUI[] amounts;

    public TextMeshProUGUI[] names;

    public GameObject[] slots;

    public Image popupImage;

    public TextMeshProUGUI popupName;

    public TextMeshProUGUI popupDescription;

    public bool inventoryOpen;

    public bool popupOpen;

    private int popupIndex;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            images[i] = slots[i].GetComponent<Image>();
        }
    }

    public void ShowPreview(int index)
    {
        if (GameObject.Find("Player").GetComponent<InventoryManager>().currentItems[index].type == InventoryManager.ItemType.none || !inventoryOpen)
        {
            return;
        }
        if (!popupOpen)
        {
            popupIndex = index;
            popupImage.sprite = GameObject.Find("Player").GetComponent<InventoryManager>().currentItems[index].icon;
            popupName.text = GameObject.Find("Player").GetComponent<InventoryManager>().currentItems[index].name;
            popupDescription.text = GameObject.Find("Player").GetComponent<InventoryManager>().currentItems[index].description;
            GetComponent<Animation>().Play("OpenPopup");
            popupOpen = true;
        }
        else
        {
            GetComponent<Animation>().Play("ClosePopup");
            popupOpen = false;
        }
    }

    public void EquipItem()
    {
        GetComponent<Animation>().Play("ClosePopup");
        popupOpen = false;
        GameObject.Find("Player").GetComponent<InventoryManager>().ChangeItem(popupIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryOpen)
        {
            for(int i = 0; i < 24; i++)
            {
                images[i].sprite = GameObject.Find("Player").GetComponent<InventoryManager>().currentItems[i].icon;
                names[i].text = GameObject.Find("Player").GetComponent<InventoryManager>().currentItems[i].name;
                if (GameObject.Find("Player").GetComponent<InventoryManager>().currentItems[i].type != InventoryManager.ItemType.none)
                {
                    amounts[i].text = GameObject.Find("Player").GetComponent<InventoryManager>().stacks[i] + "";
                }
                else
                {
                    amounts[i].text = "";
                }
            }
        }
    }
}
