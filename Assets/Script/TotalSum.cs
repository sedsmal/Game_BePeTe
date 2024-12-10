using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeStage.AntiCheat.Storage;
using ArabicSupport;

public class TotalSum : MonoBehaviour
{

    public bool isCountReport;
    public bool isMarkReport;
    private int totalTime, totalCount;
    // Start is called before the first frame update
    void Start()
    {

        if (isCountReport)
        {
            int total = 0;
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("A" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("B" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("P" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("T" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("C3" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("J" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Ch" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("H" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Kh" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("D" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Zl" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("R" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Zr" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("S" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Sh" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Sd" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Zd" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("T2" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Z2" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Eyn" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Ghyn" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("F" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Gh" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("K" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("G" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("L" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("M" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("N" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("V" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("H" + i.ToString() + "Count");
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Y" + i.ToString() + "Count");
            }
            GetComponent<Text>().text = "(" + ArabicFixer.Fix(total.ToString() + " بار ", true, true) + ")";
            totalCount = total;
        }

        if (isMarkReport)
        {
            if (totalCount != 0) {
                int toatlTime = totalTime / totalCount;
                int hour = toatlTime / 3600;
                int min = (toatlTime % 3600) / 60;
                int sec = (toatlTime % 3600) % 60;

                GetComponent<Text>().text = "(" + ArabicFixer.Fix(hour.ToString(), true, true) +
                    ":" + ArabicFixer.Fix(min.ToString(), true, true) + "':" + ArabicFixer.Fix(sec.ToString() , true, true) + "''" + ")";
            }
            else
            {
                GetComponent<Text>().text = "(" + ArabicFixer.Fix("بدون نمره", true, true) + ")";
            }

            
        }
        if (!isMarkReport && !isCountReport)
        {
            //
            int total = 0;
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("A" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("B" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("P" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("T" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("C3" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("J" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Ch" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("H" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Kh" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("D" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Zl" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("R" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Zr" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("S" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Sh" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Sd" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Zd" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("T2" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Z2" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Eyn" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Ghyn" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("F" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Gh" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("K" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("G" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("L" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("M" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("N" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("V" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("H" + i.ToString());
            }
            for (int i = 0; i < 9; i++)
            {
                total += ObscuredPrefs.GetInt("Y" + i.ToString());
            }
            int hour = total / 3600;
            int min = (total % 3600) / 60;
            int sec = (total % 3600) % 60;
            totalTime = total;
            GetComponent<Text>().text = "(" + ArabicFixer.Fix(hour.ToString(), true, true) +
                ":" + ArabicFixer.Fix(min.ToString(), true, true) + "':" + ArabicFixer.Fix(sec.ToString() , true, true) + "''" + ")";
        }
    }
}
