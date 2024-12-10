using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BizzyBeeGames;
using DG.Tweening;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.Storage;

public class VRManagment : SingletonComponent<VRManagment>
{
    public Image[] goals;
    public GameObject winPopup;
    int count;
    public GameObject HelpFinger;
    public ParticleSystem celebrationParticle;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Image a in goals)
        {
            a.color = new Color(1, 1, 1, 0.3f);
        }
        StartCoroutine(PlaySound("LevelIntro"));
        winPopup.SetActive(false);

        Timer.Instance.beginTimer();
    }

    // Update is called once per frame
    void Update()
    {
        //int time = Timer.Instance.WhatTimeIsIt();
        //Debug.Log("Time: " + time);
    }

    public void PlaySoundImmediately(string soundname)
    {
        SoundManager.Instance.Play(soundname);
    }

    IEnumerator PlaySound(string soundname)
    {
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.Play(soundname);
    }

    public void FindAlphabet()
    {
        count++;
        goals[count - 1].color = new Color(1, 1, 1, 1);
        goals[count - 1].transform.DOShakeScale(0.4f, 0.1f, 5, 90);
        SoundManager.Instance.Play("alphabet");
        SoundManager.Instance.Play("win");
        if (count % 2 == 0)
        {
            if (UnityEngine.Random.Range(0, 10) < 6)
            {
                StartCoroutine(PlaySound("Ali"));
            }
        }
        else
        {
            if (UnityEngine.Random.Range(0, 10) < 6)
            {
                StartCoroutine(PlaySound("Afarin"));
            }
        }
        
        HideHelp();
        if (count == goals.Length)
        {
            StartCoroutine(ShowWin());
        }
    }

    IEnumerator ShowWin()
    {
        yield return new WaitForSeconds(0.7f);
        StartCoroutine(PlaySound("like3"));
        winPopup.SetActive(true);
        celebrationParticle.Play();
        winPopup.transform.DOShakeScale(0.6f, 0.1f, 5, 90);
        int star = ObscuredPrefs.GetInt("st");
        ObscuredPrefs.SetInt("st", star + 7);
        CalculateTime();
    }


    public void LoadLevel(string levelName)
    {
        Loading.Instance.ShowLoading(levelName);
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
