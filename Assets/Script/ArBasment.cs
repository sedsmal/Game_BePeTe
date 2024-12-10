using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BizzyBeeGames;
public class ArBasment : SingletonComponent<ArBasment>
{
    public GameObject PlayableBalls;
    private Vector3 defaultScale;
    // Start is called before the first frame update
    public void StartGame()
    {
        Debug.Log("game start");
        //StartCoroutine(StartGameIE());
        PlayableBalls.SetActive(true);

        for (int i = 0; i < PlayableBalls.transform.childCount; i++)
        {


            defaultScale = PlayableBalls.transform.GetChild(i).localScale;
            PlayableBalls.transform.GetChild(i).localScale = Vector3.zero;
            PlayableBalls.transform.GetChild(i).DOScale(defaultScale, UnityEngine.Random.Range(0.4f, 0.9f)).OnComplete(() =>
            {
                transform.DOShakePosition(1f, 0.1f, 1, 1);

            });
        }

    }
    IEnumerator StartGameIE()
    {
        yield return new WaitForSeconds(0.5f);
        PlayableBalls.SetActive(true);

        for (int i = 0; i < PlayableBalls.transform.childCount; i++)
        {


            defaultScale = PlayableBalls.transform.GetChild(i).localScale;
            PlayableBalls.transform.GetChild(i).localScale = Vector3.zero;
            PlayableBalls.transform.GetChild(i).DOScale(defaultScale, UnityEngine.Random.Range(0.4f, 0.9f)).OnComplete(() =>
            {
                transform.DOShakePosition(1f, 0.1f, 1, 1);

            });
        }

    }

    private void Start()
    {
        PlayableBalls.SetActive(true);

        for (int i = 0; i < PlayableBalls.transform.childCount; i++)
        {
            defaultScale = PlayableBalls.transform.GetChild(i).localScale;
        }

        PlayableBalls.SetActive(false);
    }
}
