using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEvents : MonoBehaviour
{

    public GameObject[] roomTriggers;

    public Image DialogueBox;

    public Image MegaNuke;

    public TextMeshProUGUI dialogueText;

    public bool[] eventsDone =
    {
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false
    };

    public static GameEvents GetInstance()
    {
        return GameObject.Find("GameEventController").GetComponent<GameEvents>();
    }

    public IEnumerator waitForNukeToSaySomething()
    {
        if (eventsDone[1])
        {
            yield break;
        }
        yield return new WaitForSeconds(10f);
        GameObject.Find("Dialogue").GetComponent<DialogueController>().StartDialogue("ok, I should find somewhere to sleep. glad I at least ended up here instead of hospital or coliseum or something.", true);
        GameObject.Find("TO-DO list").GetComponent<TODO>().AddToTodo("find somewhere to sleep");
        RoomTriggers(true, false);
        eventsDone[1] = true;
    }

    public void RoomTriggers(bool b, bool b2)
    {
        foreach(GameObject obj in roomTriggers)
        {
            obj.SetActive(b);
        }
        if (b2)
        {
            eventsDone[5] = true;
        }
    }

    public IEnumerator AllYourFault()
    {
        GameObject.Find("Dialogue").GetComponent<DialogueController>().textSound = Resources.Load<AudioClip>("starving/itsyourfault");
        DialogueBox.sprite = Resources.Load<Sprite>("starving/dialogueboxcreepy");
        MegaNuke.sprite = Resources.Load<Sprite>("starving/meganukestarving");
        dialogueText.color = new Color(0.8f, 0f, 0f, 1f);
        DialogueController.GetInstance().StartDialogue("IT'S ALL YOUR FAULT", true);
        yield return new WaitForSeconds(1f);
        Application.LoadLevel("starved");
    }

    public IEnumerator notPoisonous()
    {
        DialogueController.GetInstance().StartDialogue("ok, it doesn't taste poisonous, but you never know. just gotta wait and see.", false);
        eventsDone[0] = true;
        yield return new WaitForSeconds(100f);
        GameObject.Find("Dialogue").GetComponent<DialogueController>().StartDialogue("alright, I guess that mushroom was fine. maybe I should make a farm!", true);
        GameObject.Find("TO-DO list").GetComponent<TODO>().AddToTodo("make a mushroom farm");
    }

    public void DoGameEvent(int gameEvent)
    {
        switch(gameEvent)
        {
            case 4:
            StartCoroutine(AllYourFault());
            return;

            case 3:
            StartCoroutine(notPoisonous());
            return;

            case 2:
            RoomTriggers(false, true);
            return;

            case 1:
            StartCoroutine(waitForNukeToSaySomething());
            return;

            case 0:
            Debug.LogError("nice bro. you put 0. tremendous job...");
            return;
        }
    }
}
