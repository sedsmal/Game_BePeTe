using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.Storage;

public class LockManager : SingletonComponent<LockManager>
{
    public GameObject answare1, answare2, answare3, answare4, answare5;
    private int progress;
    public Sprite lockOpen;
   [HideInInspector]public int trueAnsware = 0;
    public GameObject winPopup;
    public GameObject HelpFinger;
    public ParticleSystem celebrationParticle;

    private void Start()
    {
        progress = 0;
        winPopup.SetActive(false);
        answare1.SetActive(false);
        answare2.SetActive(false);
        answare3.SetActive(false);
        answare4.SetActive(false);
        answare5.SetActive(false);
        HideHelp();
        Timer.Instance.beginTimer();
        StartCoroutine(PlaySound("LevelIntro"));

    }

    public void StartPlay()
    {
        SoundManager.Instance.Play("Ready");
        answare1.SetActive(true);
        answare2.SetActive(true);
        answare3.SetActive(true);
        answare4.SetActive(true);
        answare5.SetActive(true);

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

    public IEnumerator PlaySound(string soundname)
    {
        yield return new WaitForSeconds(1.2f);
        SoundManager.Instance.Play(soundname);
    }

    public void Win()
    {
        trueAnsware++;

        if (trueAnsware == 5)
        {
            Debug.Log(trueAnsware);
            StartCoroutine(ShowWinPopup());
        }
    }

    IEnumerator ShowWinPopup()
    {
        yield return new WaitForSeconds(0.6f);
        
        StartCoroutine(PlaySound("like4"));
        winPopup.SetActive(true);
        celebrationParticle.Play();

        int star = ObscuredPrefs.GetInt("st");
        ObscuredPrefs.SetInt("st", star + 7);
        CalculateTime();

    }
    public void OpenLock(string name)
    {
        Debug.Log(name);

        GameObject.Find(name).GetComponent<SpriteRenderer>().sprite = lockOpen;
    }

    public void HideHelp()
    {
        HelpFinger.SetActive(false);
    }

    public void ShowHelp()
    {
        StartCoroutine(StartHelp());
    }

    IEnumerator StartHelp()
    {
        yield return new WaitForSeconds(0.5f);
        HelpFinger.SetActive(true);
    }

    public void DestoryAnimator()
    {
        GetComponent<Animator>().enabled = false;
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
