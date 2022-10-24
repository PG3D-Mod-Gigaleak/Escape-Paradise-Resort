using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TODO : MonoBehaviour
{
    public GameObject[] textSlots;

    public int dialogueIndex;

    public void AddToTodo(string task)
    {
        textSlots[dialogueIndex].GetComponent<TextMeshProUGUI>().text = task;
        textSlots[dialogueIndex].GetComponent<Animation>().Play("AddTask");
        dialogueIndex++;
        GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("sounds/todoadd"));
    }

    public int FindEmptySpace()
    {
        for (int i = 0; i < textSlots.Length; i++)
        {
            if (textSlots[i] == null)
            {
                return i;
            }
        }
        //this is helpful because null reference exception.
        return textSlots.Length + 1;
    }

    public void RemoveFromTodo(int index)
    {
        textSlots[index].GetComponent<Animation>().Play("RemoveTask");
        dialogueIndex = index;
        GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("sounds/todoremove"));
    }

    public void FindTaskAndRemoveFromTodo(string str)
    {
        for (int i = 0; i < textSlots.Length; i++)
        {
            if (textSlots[i].GetComponent<TextMeshProUGUI>().text == str)
            {
                RemoveFromTodo(i);
                return;
            }
        }
        Debug.LogError("found no entry in the todo list for " + '"' + str + '"');
    }
}
