using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool GamePaused()
    {
        return GameObject.Find("Inventory").GetComponent<Inventory>().inventoryOpen || GameObject.Find("CraftIndicatorPopup").GetComponent<CraftIndicator>().CIOpened;
    }
}
