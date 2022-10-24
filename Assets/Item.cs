using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite icon;

    public int stackSize;

    public InventoryManager.ItemType type;

    public string description;

    public string name;

    public GameObject prefab;
}
