﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BizzyBeeGames;
using DG.Tweening;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.Storage;
using Newtonsoft.Json;

public class FruiteManager : SingletonComponent<FruiteManager>,ISaveable
{
    public GameObject[] fruites;
    public Image[] goals;
    float timePassed = 0f;
    int count;
    public GameObject fruitParentObject;
    public GameObject winPopup;
    public GameObject HelpFinger;
    public ParticleSystem celebrationParticle;
    GameObject basket;
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
    // Start is called before the first frame update
    void Start()
    {
        basket = GameObject.Find("Basket");
        count = 0;
        foreach (Image a in goals)
        {
            a.color = new Color(0, 0, 0, 0.3f);
        }
        StartCoroutine(PlaySound("LevelIntro"));
        winPopup.SetActive(false);
        ShowHelp();
        Timer.Instance.beginTimer();
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

    private void Update()
    {
        if (count < goals.Length)
        {
            timePassed += Time.deltaTime;
            if (timePassed > 1.5f)
            {
                CreateBallones();
                timePassed = 0f;
            }
        }

    }

    private void CreateBallones()
    {
        int count = Random.Range(1, 2);
        for (int i = 0; i < count; i++)
        {
            GameObject ballone = fruites[Random.Range(0, fruites.Length)];
            Vector3 position = new Vector3(transform.position.x + Random.Range(-6, 6), transform.position.y, transform.position.z);
            GameObject newBallone = Instantiate(ballone, position, Quaternion.identity);
            newBallone.transform.SetParent(fruitParentObject.transform);
        }


    }

    public void CollectFruit()
    {
        count++;
        goals[count - 1].color = new Color(1, 1, 1, 1);
        goals[count - 1].transform.DOShakeScale(0.4f, 0.1f, 5, 90);

        if (count == goals.Length)
        {
            CalculateTime();
            StartCoroutine(ShowWin());
        }
    }

    IEnumerator ShowWin()
    {
        for (int i = 0; i < fruitParentObject.transform.childCount; i++)
        {
            Destroy(fruitParentObject.transform.GetChild(i).gameObject);
        }
        Destroy(basket);

        yield return new WaitForSeconds(0.7f);
        SoundManager.Instance.Play("win");

        int star = ObscuredPrefs.GetInt("st");
        ObscuredPrefs.SetInt("st", star + 7);

        StartCoroutine(PlaySound("like3"));
        celebrationParticle.Play();
        winPopup.SetActive(true);
        winPopup.transform.DOShakeScale(0.6f, 0.1f, 5, 90);
       
        
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
