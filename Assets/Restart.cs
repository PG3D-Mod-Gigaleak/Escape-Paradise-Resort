using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitForTheSpookyToEnd());
    }
    public IEnumerator waitForTheSpookyToEnd()
    {
        yield return new WaitForSeconds(27f);
        Application.LoadLevel("Paradise");
    }
}
