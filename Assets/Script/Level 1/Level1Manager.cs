using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.Storage;

public class Level1Manager : SingletonComponent<Level1Manager>
{
    public GameObject answare1, answare2;
    private int progress;
    int trueAnsware = 0;
    public GameObject winPopup;
    public GameObject HelpFinger;
    public ParticleSystem celebrationParticle;

    private void Start()
    {
        progress = 0;
        winPopup.SetActive(false);
        answare1.SetActive(false);
        answare2.SetActive(false);
        HideHelp();
        StartCoroutine(PlaySound("LevelIntro"));
        SoundManager.Instance.Play("music1");
        Timer.Instance.beginTimer();
    }

    public void StartPlay()
    {
        SoundManager.Instance.Play("Ready");
        answare1.SetActive(true);
        answare2.SetActive(true);
    }

    public void Progress()
    {
        progress++;
        SoundManager.Instance.Play("win");
        
        //SoundManager.Instance.Play("Afarin");
        if (progress == 1)
        {
            SoundManager.Instance.Play("like");
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

    public void PlaySoundImmediately(string soundname)
    {
        SoundManager.Instance.Play(soundname);
    }
    public void PlaySoundDelay(string soundname)
    {
        StartCoroutine(PlaySound(soundname));
    }
    public IEnumerator PlaySound(string soundname)
    {
        yield return new WaitForSeconds(1.2f);
        SoundManager.Instance.Play(soundname);
    }

    public void Win()
    {
        trueAnsware++;
        
        if (trueAnsware == 2)
        {

            StartCoroutine(ShowWin());
        }
    }

    IEnumerator ShowWin()
    {
        yield return new WaitForSeconds(0.7f);
        winPopup.SetActive(true);
        celebrationParticle.Play();
        StartCoroutine(PlaySound("like"));
        CalculateTime();
        int star = ObscuredPrefs.GetInt("st");
        ObscuredPrefs.SetInt("st", star + 7);
    }

    public void HideHelp()
    {
        HelpFinger.SetActive(false);
    }

    public void ShowHelp()
    {
        //StartCoroutine(StartHelp());
        StartHelPImed();
    }

    IEnumerator StartHelp()
    {
        yield return new WaitForSeconds(0.5f);
        HelpFinger.SetActive(true);
    }

    public void StartHelPImed()
    {
        HelpFinger.SetActive(true);
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
