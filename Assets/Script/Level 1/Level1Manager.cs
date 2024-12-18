using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.Storage;
using Newtonsoft.Json;

public class Level1Manager : SingletonComponent<Level1Manager>,ISaveable
{
    public GameObject answare1, answare2;
    private int progress;
    int trueAnsware = 0;
    public GameObject winPopup;
    public GameObject HelpFinger;
    public ParticleSystem celebrationParticle;

    #region Analyzer
    int countOfPlay;
    int lastTimeOfPlay;
    int totalTimeOfPlay;
    int bestTimeOfPlay;
    #endregion

    #region Properties
    JSONNode savedJson;
    public string SaveId { get { return SceneManager.GetActiveScene().name + " training"; } }
    #endregion

    private void Awake()
    {
        base.Awake();
        SaveManager.Instance.Register(this);
        if (!LoadSave())
        {
            countOfPlay = 0;
            lastTimeOfPlay = 0;
            bestTimeOfPlay = 0;
            totalTimeOfPlay = 0;
            Save();

            // SaveManager.Instance.SaveNow();
            savedJson = SaveManager.Instance.DeSerialize99();

        }
        else
        {
            savedJson = SaveManager.Instance.DeSerialize99();
        }
    }

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
            CalculateTime();
            SaveManager.Instance.SaveNow();
            StartCoroutine(ShowWin());
        }
    }

    IEnumerator ShowWin()
    {
        yield return new WaitForSeconds(0.7f);
        winPopup.SetActive(true);
        celebrationParticle.Play();
        StartCoroutine(PlaySound("like"));
        
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

        countOfPlay = countOfPlay + 1;
        totalTimeOfPlay = time + totalTimeOfPlay;
        if (time < bestTimeOfPlay && bestTimeOfPlay != 0)
        {
            bestTimeOfPlay = time;
        }
        lastTimeOfPlay = time;

        Save();
        SaveManager.Instance.SaveNow();
        //countOfPlay = ObscuredPrefs.GetInt(SceneManager.GetActiveScene().name + "Count");
        //ObscuredPrefs.SetInt(SceneManager.GetActiveScene().name, time + lastTimeOfPlay);
        //ObscuredPrefs.SetInt(SceneManager.GetActiveScene().name + "Count", countOfPlay + 1);
    }


    #region Save Methods

    public Dictionary<string, object> Save()
    {
        Dictionary<string, object> json;

        if (savedJson == null || savedJson == "")
        {

            json = new Dictionary<string, object>();
        }
        else
        {
            if (savedJson[SaveId] == null || savedJson[SaveId] == "")
            {
                json = new Dictionary<string, object>();
            }
            else
            {
                json = JsonConvert.DeserializeObject<Dictionary<string, object>>(savedJson[SaveId].ToString());
            }

        }

        json["lastTimeOfPlay"] = lastTimeOfPlay;
        json["bestTimeOfPlay"] = bestTimeOfPlay;
        json["totalTimeOfPlay"] = totalTimeOfPlay;
        json["countOfPlay"] = countOfPlay;
        //json["lotterylefttime"] = leftTime;

        return json;
    }

    public bool LoadSave()
    {
        JSONNode json = SaveManager.Instance.LoadSave(this);

        if (json == null)
        {

            return false;
        }

        countOfPlay = int.Parse(json["countOfPlay"].Value);
        lastTimeOfPlay = int.Parse(json["lastTimeOfPlay"].Value);
        bestTimeOfPlay = int.Parse(json["bestTimeOfPlay"].Value);
        totalTimeOfPlay = int.Parse(json["totalTimeOfPlay"].Value);
        return true;
    }
    #endregion
}
