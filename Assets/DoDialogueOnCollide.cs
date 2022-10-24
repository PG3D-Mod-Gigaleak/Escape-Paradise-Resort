using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDialogueOnCollide : MonoBehaviour
{
    public bool stack;

    public string[] say;

    private bool hasDone;

    void OnTriggerEnter()
    {
        if (!hasDone)
        {
            if (!stack)
            {
                GameObject.Find("Dialogue").GetComponent<DialogueController>().StartDialogue(say[0], true);
            }
            else
            {
                for (int i = 0; i < say.Length; i++)
                {
                    GameObject.Find("Dialogue").GetComponent<DialogueController>().StartDialogue(say[i], true);
                }
            }
            hasDone = true;
        }
    }
}
