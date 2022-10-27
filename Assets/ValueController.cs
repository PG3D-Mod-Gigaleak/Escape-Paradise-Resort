using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ValueController : MonoBehaviour
{
    public float Hunger;

    public float time;

    private Image HungerSlider;

    private bool doneTheNight;


    void Start()
    {
        HungerSlider = GameObject.Find("HungerFiller").GetComponent<Image>();
        HungerSlider.fillAmount = 0.3f;
    }

    public IEnumerator GetRidOfBedAndAddBedAndSleep()
    {
        GameObject.Find("TO-DO list").GetComponent<TODO>().FindTaskAndRemoveFromTodo("find somewhere to sleep");
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("TO-DO list").GetComponent<TODO>().AddToTodo("find somewhere to sleep and go to sleep");
    }

    public void UpdateGameWithHunger()
    {
        if (time >= 1f && !doneTheNight)
        {
            GameObject.Find("Music").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("sounds/nightmusic");
            GameObject.Find("Music").GetComponent<AudioSource>().Play();
            if (GameEvents.GetInstance().eventsDone[5])
            {
                DialogueController.GetInstance().StartDialogue("*yawn* welp, time to go to sleep. today was (placeholder), but tomorrow is a new day.", true);
                GameObject.Find("TO-DO list").GetComponent<TODO>().AddToTodo("go to sleep");
            }
            else
            {
                DialogueController.GetInstance().StartDialogue("*yawn* welp, time to go find a bed. probably should have done that a bit sooner. today was (placeholder), but tomorrow is a new day.", true);
                StartCoroutine(GetRidOfBedAndAddBedAndSleep());
            }
            doneTheNight = true;
        }
        foreach (Material mat in Resources.FindObjectsOfTypeAll<Material>())
        {
            if (mat.shader.name == "Legacy Shaders/Lightmapped/Diffuse")
            {
                Debug.Log("found lightmapped diffuse");
                if (time <= 0.9f)
                {
                    mat.SetColor("_Color", new Color(1f-time, 1f-time, 1f-time, 1f));
                }
            }
            GameObject.Find("sky_02").GetComponent<MeshRenderer>().material.SetFloat("_BlendAlpha", time);
            GameObject.Find("Directional light").GetComponent<Light>().intensity = 0.9f - time;
        }
        if (Hunger > 0.75f)
        {
            GameObject.Find("Music").GetComponent<AudioSource>().pitch = 1f;
            GameObject.Find("StaticImage").GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0f);
            GameObject.Find("static").GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 0f);
            GameObject.Find("StarveImage").GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0);
            GameObject.Find("starve").GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 0);
            return;
        }
        if (Hunger > 0.5f)
        {
            GameObject.Find("Music").GetComponent<AudioSource>().pitch = Hunger + 0.25f;
            GameObject.Find("StaticImage").GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0f);
            GameObject.Find("static").GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 0f);
            GameObject.Find("StarveImage").GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0);
            GameObject.Find("starve").GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 0);
            return;
        }
        if (Hunger > 0.25f)
        {
            GameObject.Find("Music").GetComponent<AudioSource>().pitch = Hunger;
            GameObject.Find("StaticImage").GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0f);
            GameObject.Find("static").GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 0f);
            GameObject.Find("StarveImage").GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0);
            GameObject.Find("starve").GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 0);
            if (!GameEvents.GetInstance().eventsDone[2])
            {
                DialogueController.GetInstance().StartDialogue("ugh, I dont feel so good. I need to eat something.", true);
                GameEvents.GetInstance().eventsDone[2] = true;
            }
            return;
        }
        if (Hunger > 0.1f)
        {
            GameObject.Find("StaticImage").GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0 - Hunger + 0.5f);
            GameObject.Find("static").GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 0 - Hunger + 0.5f);
            if (!GameEvents.GetInstance().eventsDone[3])
            {
                DialogueController.GetInstance().StartDialogue("my vision is getting blurry, I need to get something to eat. NOW!", true);
                GameEvents.GetInstance().eventsDone[3] = true;
            }
            return;
        }
        if (Hunger > 0f)
        {
            GameObject.Find("StaticImage").GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0 - Hunger + 0.65f);
            GameObject.Find("static").GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 0 - Hunger + 0.65f);
            GameObject.Find("StarveImage").GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0 + Hunger + 0.5f);
            GameObject.Find("starve").GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 0 - Hunger + 0.5f);
            if (!GameEvents.GetInstance().eventsDone[4])
            {
                DialogueController.GetInstance().StartDialogue("PLEASE! ISN'T THERE ANY FOOD OUT HERE!?", true);
                GameEvents.GetInstance().eventsDone[4] = true;
            }
        }
        if (Hunger <= 0f)
        {
            GameEvents.GetInstance().DoGameEvent(4);
        }
    }

    void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyDown("l"))
        {
            Hunger -= 0.01f;
        }
        if (Input.GetKeyDown("k"))
        {
            time += 0.01f;
        }
        #endif
        if (Hunger > 1f)
        {
            Hunger = 1f;
        }
        HungerSlider.fillAmount = Hunger;
        Hunger -= 0.00001f;
        if (time < 1f)
        {
            time += 0.000001f;
        }
        if (time > 1f)
        {
            time = 1f;
        }
        UpdateGameWithHunger();
    }
}
