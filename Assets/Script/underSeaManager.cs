using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.Storage;

public class underSeaManager : SingletonComponent<underSeaManager>
{

    private int progress;
    int trueAnsware = 0;
    public GameObject winPopup;
    public GameObject HelpFinger;
    public ParticleSystem celebrationParticle;

    private void Start()
    {
        progress = 0;
        winPopup.SetActive(false);
        StartCoroutine(PlaySound("LevelIntro"));
        StartCoroutine(StartHelp());
        Timer.Instance.beginTimer();
    }

    public void StartPlay()
    {
        SoundManager.Instance.Play("Ready");
    }

    public void Progress()
    {
        progress++;

        if (progress == 1)
        {
            SoundManager.Instance.Play("like2");
        }
    }

    public void AlphabetRealease()
    {
        SoundManager.Instance.Play("Start Level");
    }

    public void LoadLevel(string levelName)
    {
        Loading.Instance.ShowLoading(levelName);
    }

    public IEnumerator PlaySound(string soundname)
    {
        yield return new WaitForSeconds(1.1f);
        SoundManager.Instance.Play(soundname);
    }

    public void Win()
    {
        trueAnsware++;

        if (trueAnsware == 5)
        {
            StartCoroutine(ShowWinPopup());
        }
    }

    IEnumerator ShowWinPopup()
    {
        yield return new WaitForSeconds(0.4f);
        winPopup.SetActive(true);
        CalculateTime();
        StartCoroutine(PlaySound("like4"));
        celebrationParticle.Play();

        int star = ObscuredPrefs.GetInt("st");
        ObscuredPrefs.SetInt("st", star + 7);

    }

    public void HideHelp()
    {
        HelpFinger.SetActive(false);
    }

    IEnumerator StartHelp()
    {
        yield return new WaitForSeconds(0.5f);
        HelpFinger.SetActive(true);
    }
    public void PlaySoundImmediately(string soundname)
    {
        SoundManager.Instance.Play(soundname);
    }

    public void CalculateTime()
    {
        int sec = Timer.Instance.WhatTimeIsIt();
        int min = Timer.Instance.WhatMinIsIt();
        int time = min * 60 + sec;
        int count = ObscuredPrefs.GetInt(SceneManager.GetActiveScene().name + "Count");
        int lastTime = ObscuredPrefs.GetInt(SceneManager.GetActiveScene().name);
        ObscuredPrefs.SetInt(SceneManager.GetActiveScene().name, time + lastTime);
        ObscuredPrefs.SetInt(SceneManager.GetActiveScene().name + "Count", count + 1);
    }
}
