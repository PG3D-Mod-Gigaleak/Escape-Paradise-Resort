using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            if (!CraftMenu.GetInstance().CMOpened)
            {
                CraftMenu.GetInstance().OpenCM();
            }
            else
            {
                CraftMenu.GetInstance().CloseCM();
            }
        }
    }
}
