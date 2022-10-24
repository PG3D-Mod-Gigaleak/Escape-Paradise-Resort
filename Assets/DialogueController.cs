using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI text;

    public AudioClip textSound;

    public bool dialogueUp;

    private bool firstStackDone;

    private bool starting;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "";
        StartCoroutine(DoDialogue("it's been 3 weeks. I miss my player. I can't even leave paradise resort anymore."));
        StartCoroutine(StackDialogue("I'll starve if i dont find food soon. so I'd better get moving.", true));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("e"))
        {
            StartCoroutine(CloseDialogue());
        }
        if (firstStackDone)
        {
            GameObject.Find("TO-DO list").GetComponent<TODO>().AddToTodo("find some food");
            firstStackDone = false;
        }
    }

    public IEnumerator DoDialogue(string message)
    {
        while (GameController.GamePaused())
        {
            yield return new WaitForSeconds(0.1f);
        }
        text.text = "";
        starting = false;
        dialogueUp = true;
        GetComponent<Animation>().Play("DialogueOn");
        yield return new WaitForSeconds(0.1f);
        if(GetComponent<Animation>().IsPlaying("DialogueOff"))
        {
            yield break;
        }
        starting = true;
        int index = 0;
        while(index != message.Length && starting && !GetComponent<Animation>().IsPlaying("DialogueOff"))
        {
            text.text += message[index];
            GetComponent<AudioSource>().PlayOneShot(textSound);
            yield return new WaitForSeconds(GetLetterTime(message[index]));
            index++;
        }
        if (!starting)
        {
            text.text = "";
        }
    }

    public float GetLetterTime(char c)
    {
        switch(c)
        {
            case '.':
            return 0.5f;
            case '!':
            return 0.5f;
            case ',':
            return 0.35f;
            default:
            return 0.05f;
        }
    }

    public IEnumerator StackDialogue(string message, bool firstStack)
    {
        while (dialogueUp)
        {
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(DoDialogue(message));
        if (firstStack)
        {
            yield return new WaitForSeconds(2f);
            firstStackDone = true;
        }
    }

    public void StartDialogue(string message, bool stack)
    {
        if (stack)
        {
           StartCoroutine(StackDialogue(message, false));
        }
        else
        {
            StartCoroutine(DoDialogue(message));
        }
    }

    public IEnumerator CloseDialogue()
    {
        if (!dialogueUp)
        {
            yield break;
        }
        GetComponent<Animation>().Play("DialogueOff");
        yield return new WaitForSeconds(0.1f);
        text.text = "";
        dialogueUp = false;
    }
}
