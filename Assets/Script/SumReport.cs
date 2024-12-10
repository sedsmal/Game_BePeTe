using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeStage.AntiCheat.Storage;
using ArabicSupport;

public class SumReport : MonoBehaviour
{
    public string id;
    public bool isCountReport;
    public bool isMarkReport;
    // Start is called before the first frame update
    void Start()
    {

        if (isCountReport)
        {
            int total = 0;
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt(id + i.ToString() + "Count");
            }
            GetComponent<Text>().text = ArabicFixer.Fix(total.ToString() + " بار ", true, true);
        }

        if (isMarkReport)
        {
            int totalCount = 0;
            for (int i = 0; i < 9; i++)
            {
                totalCount += ObscuredPrefs.GetInt(id + i.ToString() + "Count");
            }
            int totalTime = 0;
            for (int i = 0; i < 9; i++)
            {
                totalTime += ObscuredPrefs.GetInt(id + i.ToString());
            }

            if (totalCount != 0)
            {
                int toatlTime = totalTime / totalCount;
                int hour = toatlTime / 3600;
                int min = (toatlTime % 3600) / 60;
                int sec = (toatlTime % 3600) % 60;

                if (min <= 7) {
                    GetComponent<Text>().text = "ﺏﻮﺧ ﯽﻠﯿﺧ";
                }else if (min > 7 && min <= 10)
                {
                    GetComponent<Text>().text = "ﺏﻮﺧ";
                }else if(min > 10)
                {
                    GetComponent<Text>().text = "ﻂﺳﻮﺘﻣ";
                }

                //GetComponent<Text>().text = ArabicFixer.Fix(hour.ToString(), true, true) +
                //    ":" + ArabicFixer.Fix(min.ToString(), true, true) + "':" + ArabicFixer.Fix(sec.ToString(), true, true) + "''";
            }
            else { GetComponent<Text>().text = ArabicFixer.Fix("بدون نمره", true, true); }

            
        }
        if (!isMarkReport && !isCountReport)
        {
            //
            int total=0;
            for (int i = 0; i < 9; i++) {
                total += ObscuredPrefs.GetInt(id + i.ToString());
                    }

            
            int hour = total / 3600;
            int min = (total % 3600) / 60;
            int sec = (total % 3600) % 60;

            GetComponent<Text>().text = ArabicFixer.Fix(hour.ToString(), true, true) +
                ":" + ArabicFixer.Fix(min.ToString(), true, true) + "':" + ArabicFixer.Fix(sec.ToString(), true, true) + "''";
        }
    }
}