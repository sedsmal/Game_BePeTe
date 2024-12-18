using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeStage.AntiCheat.Storage;
using ArabicSupport;
using BizzyBeeGames;

public class levelTimeReport : MonoBehaviour
{
    public string id;
    public bool isCountReport;
    public bool isMarkReport;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = ArabicFixer.Fix("بدون نمره", true, true);

        if(SaveManager.Instance.LoadSave(id + " training"))
        {
            int totalTime = int.Parse(SaveManager.Instance.LoadSave(id + " training")["totalTimeOfPlay"].Value);
            int countOfPlay = int.Parse(SaveManager.Instance.LoadSave(id + " training")["countOfPlay"].Value);

            if (isCountReport)
            {
                GetComponent<Text>().text = ArabicFixer.Fix(countOfPlay.ToString() + " بار ", true, true);
            }

            if (isMarkReport)
            {

                if (countOfPlay != 0)
                //if (ObscuredPrefs.GetInt(id + "Count") != 0)
                {
                    int toatlTime = totalTime / countOfPlay;
                    int hour = toatlTime / 3600;
                    int min = (toatlTime % 3600) / 60;
                    int sec = (toatlTime % 3600) % 60;
                    GetComponent<Text>().text = ArabicFixer.Fix(hour.ToString(), true, true) +
                    ":" + ArabicFixer.Fix(min.ToString(), true, true) + "':" + ArabicFixer.Fix(sec.ToString(), true, true) + "''";
                }
                else
                {
                    GetComponent<Text>().text = ArabicFixer.Fix("بدون نمره", true, true);
                }
            }
            if (!isMarkReport && !isCountReport)
            {


                //int hour = ObscuredPrefs.GetInt(id) / 3600;
                //int min = (ObscuredPrefs.GetInt(id) % 3600) / 60;
                //int sec = (ObscuredPrefs.GetInt(id) % 3600) % 60;
                int hour = totalTime / 3600;
                int min = (totalTime % 3600) / 60;
                int sec = (totalTime % 3600) % 60;
                GetComponent<Text>().text = ArabicFixer.Fix(hour.ToString(), true, true) +
                    ":" + ArabicFixer.Fix(min.ToString(), true, true) + "':" + ArabicFixer.Fix(sec.ToString(), true, true) + "''";
            }
        }




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
