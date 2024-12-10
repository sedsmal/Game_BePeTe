using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.Storage;
using DG.Tweening;

public class WorlPartOne : MonoBehaviour
{
    public GameObject[] answare;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < answare.Length; i++)
        {
            answare[i].SetActive(false);
        }
    }
    public void StartPlay()
    {
        SoundManager.Instance.Play("Ready");
        

        for (int i = 0; i < answare.Length; i++)
        {
            answare[i].SetActive(true);
        }
    }
    public void AlphabetRealease()
    {
        SoundManager.Instance.Play("Start Level");
        StartCoroutine(PlaySound("Intro1"));
    }
    public IEnumerator PlaySound(string soundname)
    {
        yield return new WaitForSeconds(0.6f);
        SoundManager.Instance.Play(soundname);
    }
}
