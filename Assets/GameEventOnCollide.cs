using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventOnCollide : MonoBehaviour
{
    public int gameEvent;

    void OnTriggerEnter()
    {
        GameObject.Find("GameEventController").GetComponent<GameEvents>().DoGameEvent(gameEvent);
    }
}
