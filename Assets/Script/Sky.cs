using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;

public class Sky : MonoBehaviour
{
    public GameObject rain, snow;

    private void Start()
    {
        rain.SetActive(false);
        snow.SetActive(false);
    }

    public void StartRain()
    {
        rain.SetActive(true);
        snow.SetActive(false);
        SoundManager.Instance.Play("music2-rain");
    }

    public void StartSnow()
    {
        rain.SetActive(false);
        snow.SetActive(true);
    }

    public void StopAll()
    {
        rain.SetActive(false);
        snow.SetActive(false);
        SoundManager.Instance.Stop("music2-rain");
    }
}
