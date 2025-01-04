using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using UnityEngine.UI;
using ArabicSupport;

public class AppliedBehaviourAnalyser : MonoBehaviour
{
    string id;
    public List<string> levelList;
    int countOfPlay, totalTimeOfPlay, lastTimeOfPlay, bestTimeOfPlay, totalCount;
    int TcountOfPlay, TtotalTimeOfPlay, TlastTimeOfPlay, TbestTimeOfPlay, TtotalCount = 0;
    int savedLevels = 0;

    public Text totalPlayTimeText,abaTMeanTxt;
    public Image abaTMeanSlider,abaLastMeanSlider;
    private void Start()
    {
        AllLoader();
    }
    void AllLoader()
    {
        for (int i = 0; i < levelList.Count; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                id = levelList[i].ToString() + j.ToString() + " training";


                if (SaveManager.Instance.LoadSave(id) != null)
                {
                    lastTimeOfPlay = int.Parse(SaveManager.Instance.LoadSave(id)["lastTimeOfPlay"].Value);
                    bestTimeOfPlay = int.Parse(SaveManager.Instance.LoadSave(id)["bestTimeOfPlay"].Value);
                    totalTimeOfPlay = int.Parse(SaveManager.Instance.LoadSave(id)["totalTimeOfPlay"].Value);
                    countOfPlay = int.Parse(SaveManager.Instance.LoadSave(id)["countOfPlay"].Value);
                    totalCount = totalCount + countOfPlay;
                    savedLevels++;

                    TtotalTimeOfPlay += totalTimeOfPlay;
                    TlastTimeOfPlay += lastTimeOfPlay;





                }
            }
        }
        TimeWriter(TtotalTimeOfPlay, totalPlayTimeText);
       // ABATimeWriter(TtotalTimeOfPlay, savedLevels,abaTMeanTxt,abaTMeanSlider,100);
        ABATimeWriter(TlastTimeOfPlay, savedLevels, abaTMeanTxt, abaTMeanSlider, 100);
    }

    void TimeWriter(int time,Text txt)
    {
        
        int hour = time / 3600;
        int min = (time % 3600) / 60;
        int sec = (time % 3600) % 60;

        txt.text = ArabicFixer.Fix(hour.ToString() + " : " + min.ToString() + "'", true, true);

    }

    void ABATimeWriter(int time,int count, Text txt)
    {
        int mean = time / count;
        int hour = mean / 3600;
        int min = (mean % 3600) / 60;
        int sec = (mean % 3600) % 60;

        txt.text = ArabicFixer.Fix( sec.ToString() + " ثانیه ", true, true);

    }
    void ABATimeWriter(int time, int count, Text txt,Image slider, float highSliderAmount)
    {
        ABATimeWriter(time, count, txt);
        slider.fillAmount= (((time / count) % 3600) % 60) / highSliderAmount;

    }
}
