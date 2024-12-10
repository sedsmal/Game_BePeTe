using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using ArabicSupport;
using BizzyBeeGames;



public class Timer : SingletonComponent<Timer>
{

    private TimeSpan timePlaying;
    private bool timerGoing;
    private float elapsedTime, startTime;
    static string timePlayingString;



    public void beginTimer()
    {
        timerGoing = true;
        startTime = Time.time;
        elapsedTime = 0f;
        StartCoroutine(UpdateTimer());

    }
    public string endTimer()
    {
        timerGoing = false;
        return timePlayingString;
    }
    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            timePlayingString = timePlaying.ToString("mm':'ss'.'f");
            yield return null;
        }
    }

    public int WhatTimeIsIt()
    {
        
        return Convert.ToInt32( TimeSpan.FromSeconds(elapsedTime).ToString("ss")) ;
    }
    public int WhatMinIsIt()
    {

        return Convert.ToInt32(TimeSpan.FromSeconds(elapsedTime).ToString("mm"));
    }
}
