using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.Storage;
using BizzyBeeGames;
using DG.Tweening;

using UnityEngine.SceneManagement;

public class LevelManager : SingletonComponent<LevelManager>
{
    public int puzzlePieceCount;
    public GameObject winPopup;
    public GameObject HelpFinger;
    public ParticleSystem celebrationParticle;
    private int progress;

    public ParticleSystem particle1, particle2, particle3, particle4;

    void Start()
    {
        progress = 0;
        winPopup.SetActive(false);
        StartCoroutine(PlaySound("LevelIntro"));
        Timer.Instance.beginTimer();
    }

    public void DoProgress()
    {
        progress++;
        CheckWin(progress);
        SoundManager.Instance.Play("win");

        int rand = UnityEngine.Random.Range(0, 20);
        if (rand < 3)
        {
            SoundManager.Instance.Play("afarin");
        }
        else if (rand >= 3 && rand < 5)
        {
            SoundManager.Instance.Play("ali");
        }
        else
        {
            SoundManager.Instance.Play("alphabet");
        }

        
        
    }

    void CheckWin(int progress)
    {
        if (progress == puzzlePieceCount)
        {
            
            StartCoroutine(ShowWin());
        }
    }

    IEnumerator ShowWin()
    {
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(PlaySound("like4"));

        int star = ObscuredPrefs.GetInt("st");
        ObscuredPrefs.SetInt("st", star + 7);

        winPopup.SetActive(true);
        celebrationParticle.Play();
        winPopup.transform.DOShakeScale(0.6f, 0.1f, 5, 90);
        CalculateTime();
    }


    public void LoadLevel(string levelName)
    {
        Loading.Instance.ShowLoading(levelName);
    }
    public void LoadLockLevel(string levelName)
    {
        if (ObscuredPrefs.GetString("_P")== "B1")
        {
            Loading.Instance.ShowLoading(levelName);
        }
        else { Loading.Instance.ShowLoading(levelName+"X"); }

        
    }

    public void HideHelp()
    {
        if (HelpFinger != null) { HelpFinger.SetActive(false); }
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

    public bool IsHelpActive()
    {
        return HelpFinger.activeInHierarchy;
    }

    public void PlaySoundImmediately(string soundname)
    {
        SoundManager.Instance.Play(soundname);
    }

    IEnumerator PlaySound(string soundname)
    {
        yield return new WaitForSeconds(1.2f);
        SoundManager.Instance.Play(soundname);
    }
    public void PlaySoundImmediatelyWithParticle(string soundname)
    {
        SoundManager.Instance.Play(soundname);

        if (soundname.Contains("1"))
        {
            particle1.Play();

        }else if (soundname.Contains("2"))
        {
            particle2.Play();

        }
        else if (soundname.Contains("3"))
        {

            particle3.Play();
        }
        else if (soundname.Contains("4"))
        {

            particle4.Play();
        }
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
