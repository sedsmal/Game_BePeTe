using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeStage.AntiCheat.Storage;
using ArabicSupport;

public class levelTimeReport : MonoBehaviour
{
    public string id;
    public bool isCountReport;
    public bool isMarkReport;
    // Start is called before the first frame update
    void Start()
    {
        
        if(isCountReport)
        {
            GetComponent<Text>().text = ArabicFixer.Fix(ObscuredPrefs.GetInt(id+"Count").ToString()+ " بار ", true, true);
        }

        if (isMarkReport)
        {
            if(ObscuredPrefs.GetInt(id + "Count") != 0)
            {
                int toatlTime = ObscuredPrefs.GetInt(id) / ObscuredPrefs.GetInt(id + "Count");
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
        if(!isMarkReport&& !isCountReport) 
        {
            //
            int hour = ObscuredPrefs.GetInt(id) / 3600;
            int min = (ObscuredPrefs.GetInt(id) % 3600) / 60;
            int sec = (ObscuredPrefs.GetInt(id) % 3600) % 60;
            GetComponent<Text>().text = ArabicFixer.Fix(hour.ToString(), true, true) +
                ":" + ArabicFixer.Fix(min.ToString(), true, true) + "':" + ArabicFixer.Fix(sec.ToString(), true, true) + "''";
        }
            


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
