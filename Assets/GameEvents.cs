using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public GameObject[] roomTriggers;

    public IEnumerator waitForNukeToSaySomething()
    {
        yield return new WaitForSeconds(10f);
        GameObject.Find("Dialogue").GetComponent<DialogueController>().StartDialogue("ok, I should find somewhere to sleep. glad I at least ended up here instead of hospital or coliseum or something.", true);
        GameObject.Find("TO-DO list").GetComponent<TODO>().AddToTodo("find somewhere to sleep");
        RoomTriggers(true);
    }

    public void RoomTriggers(bool b)
    {
        foreach(GameObject obj in roomTriggers)
        {
            obj.SetActive(b);
        }
    }

    public void DoGameEvent(int gameEvent)
    {
        switch(gameEvent)
        {
            case 2:
            RoomTriggers(false);
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
