using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftMenu : MonoBehaviour
{
    public CraftButton[] craftButtons;

    public static CraftMenu GetInstance()
    {
        return GameObject.Find("CraftMenu").GetComponent<CraftMenu>();
    }

    public bool CMOpened;
    
    public void OpenCM()
    {
        if (GetComponent<Animation>().isPlaying || GameObject.Find("Inventory").GetComponent<Inventory>().inventoryOpen)
        {
            return;
        }
        GetComponent<Animation>().Play("OpenCM");
        CMOpened = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseCM()
    {
        if (GetComponent<Animation>().isPlaying)
        {
            return;
        }
        GetComponent<Animation>().Play("CloseCM");
        CMOpened = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
