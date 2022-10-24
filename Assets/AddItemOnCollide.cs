using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemOnCollide : MonoBehaviour
{
    public string item;

    public int amount;

    private bool hasDone;

    public bool Destroy;

    void OnTriggerEnter()
    {
        if (!hasDone)
        {
            GameObject.Find("Player").GetComponent<InventoryManager>().AddItemVoid(item, amount);
            hasDone = true;
            if (Destroy)
            {
                StartCoroutine(destroy());
            }
        }
    }

    public IEnumerator destroy()
    {
        yield return new WaitForSeconds(0.01f);
        Destroy(this.gameObject);
    }
}
