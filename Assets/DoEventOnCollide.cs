using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoEventOnCollide : MonoBehaviour
{
    public string theObject;

    public string message;

    public int value;

    public bool useString;

    public string stringValue;

    public bool remove;

    void OnTriggerEnter()
    {
        if (!useString)
        {
            GameObject.Find(theObject).SendMessage(message, value);
        }
        else
        {
            GameObject.Find(theObject).SendMessage(message, stringValue);
        }
        if (remove)
        {
        StartCoroutine(Remove());
        }
    }

    public IEnumerator Remove()
    {
        yield return new WaitForSeconds(0.01f);
        base.gameObject.SetActive(false);
    }
}
