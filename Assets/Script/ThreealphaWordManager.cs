using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.Storage;
using DG.Tweening;

public class ThreealphaWordManager : SingletonComponent<ThreealphaWordManager>
{
    public GameObject[] answare;
    public GameObject RoundOne;
    public GameObject WordImageRound;

    public GameObject WordTextRound;
    public GameObject SortingRound;
    public GameObject finalShow;
    private int progress;
    int trueAnsware = 0;
    public GameObject winPopup;
    public GameObject HelpFinger;
    public ParticleSystem celebrationParticle;

    // Start is called before the first frame update
    void Start()
    {
        progress = 0;
        winPopup.SetActive(false);
        RoundOne.SetActive(true);
        WordImageRound.SetActive(false);
        WordTextRound.SetActive(false);
        SortingRound.SetActive(false);
        finalShow.SetActive(false);
        for (int i = 0; i < answare.Length; i++)
        {
            answare[i].SetActive(false);
        }

        HideHelp();


        
        StartCoroutine(PlaySound("Word_Slow"));
        // PlaySound("Word_Slow");
        Timer.Instance.beginTimer();
    }

    public void StartPlay()
    {
        SoundManager.Instance.Play("Ready");
        
        //StartCoroutine(PlaySound("Intro1"));
        for (int i = 0; i < answare.Length; i++)
        {
            answare[i].SetActive(true);
        }
    }

    //public void Progress()
    //{
    //    progress++;
    //    SoundManager.Instance.Play("win");

    //    if (progress == 1)
    //    {
    //        SoundManager.Instance.Play("like1");
    //    }
    //}

    public void AlphabetRealease()
    {
        SoundManager.Instance.Play("Start Level");
        //SoundManager.Instance.Play("Intro1");
        
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
        yield return new WaitForSeconds(0.6f);
        SoundManager.Instance.Play(soundname);
    }
    
    public void Win()
    {
        trueAnsware++;

        if (trueAnsware == answare.Length)
        {
            StartCoroutine(ShowWordImagePopUp());
            
            StartCoroutine( PlaySound("Word_Normal"));
            celebrationParticle.Play();

        }
    }

    public void PlayParticel()
    {
        celebrationParticle.Play();
        
    }
    public void PlayParticelLoop()
    {
        celebrationParticle.loop = true;
        celebrationParticle.Play();

    }
    public void ShowWInPopup() {
        ThreealphaWordManager.Instance.ShowFinal();
        SoundManager.Instance.Play("Word_Slow");
        PlayParticelLoop();
        StartCoroutine(ShowWinPopUp());
    }

    IEnumerator ShowWinPopUp()
    {
        yield return new WaitForSeconds(2.5f);
        
        winPopup.SetActive(true);

        //add stars
        int star = ObscuredPrefs.GetInt("st");
        ObscuredPrefs.SetInt("st", star + 7);
        

        CalculateTime();
    }
    IEnumerator ShowWordImagePopUp()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(PlaySound("Intro2"));
        SoundManager.Instance.Play("Afarin");
        WordImageRound.SetActive(true);
    }
    public void hideWOrdImage()
    {
        StartCoroutine(hideWOrdImageIe());
    }
    IEnumerator hideWOrdImageIe()
    {
        yield return new WaitForSeconds(0.5f);
        WordImageRound.SetActive(false);
        WordTextRound.SetActive(true);
        StartCoroutine(PlaySound("Intro3"));
    }
    IEnumerator ShowWordtextPopUp()
    {
        yield return new WaitForSeconds(0.3f);
        WordTextRound.SetActive(true);
    }

    public void ShowSortRound()
    {
        Debug.Log("XXXX");
        StartCoroutine(PlaySound("Intro4"));
        SortingRound.SetActive(true);
        WordImageRound.SetActive(false);
        WordTextRound.SetActive(false);
        RoundOne.SetActive(false);
        HideRoundOne();
        WordSortManagment.Instance.PlayAnimation();
    }

    IEnumerator ShowSortRoundIE()
    {
        yield return new WaitForSeconds(0.5f);
        SortingRound.SetActive(true);
        
    }
    public void HideRoundOne()
    {
        RoundOne.GetComponent<Animator>().enabled = false;
        for(int i = 0; i < RoundOne.transform.childCount; i++)
        {
            RoundOne.transform.GetChild(i).gameObject.SetActive(false);
        }
        
        //RoundOne.SetActive(false);
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

    public void ShowFinal()
    {
        finalShow.SetActive(true);
        //StartCoroutine(ShowSortRoundIE());
    }
}
