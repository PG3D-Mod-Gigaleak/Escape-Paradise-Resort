using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ValueController : MonoBehaviour
{
    public float Hunger;

    private Image HungerSlider;


    void Start()
    {
        HungerSlider = GameObject.Find("HungerFiller").GetComponent<Image>();
        HungerSlider.fillAmount = 0.3f;
    }

    public void UpdateGameWithHunger()
    {
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
            return;
        }
        if (Hunger > 0.1f)
        {
            GameObject.Find("StaticImage").GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0 - Hunger + 0.5f);
            GameObject.Find("static").GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 0 - Hunger + 0.5f);
            return;
        }
        if (Hunger > 0f)
        {
            GameObject.Find("StaticImage").GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0 - Hunger + 0.35f);
            GameObject.Find("static").GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 0 - Hunger + 0.35f);
            GameObject.Find("StarveImage").GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0 + Hunger - 0.5f);
            GameObject.Find("starve").GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 0 - Hunger + 0.5f);
        }
        if (Hunger <= 0f)
        {
            Application.LoadLevel("starved");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            #if UNITY_EDITOR
            Hunger -= 0.01f;
            #endif
        }
        if (Hunger > 1f)
        {
            Hunger = 1f;
        }
        HungerSlider.fillAmount = Hunger;
        Hunger -= 0.00001f;
        UpdateGameWithHunger();
    }
}
