using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftIndicator : MonoBehaviour
{
    public bool CIOpened;

    public GameObject[] PartTypes;

    public void Close()
    {
        if (!CIOpened)
        {
            return;
        }
        GetComponent<Animation>().Play("CIClose");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CIOpened = false;
    }
}
