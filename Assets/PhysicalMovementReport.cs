
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeStage.AntiCheat.Storage;
using ArabicSupport;
using BizzyBeeGames;

public class PhysicalMovementReport : MonoBehaviour
{
   string id;
/*    public bool isCountReport;
    public bool isMarkReport;*/
    public List<string> levelList;
    int savedLevels=0;
    int countOfPlay;
    float totalmovements;
    float lastmovements;
    float meanMovementMagnitude;
    float meanTotalMovementMagnitude;
    float totalTotalMovementMagnitude;
    float totalLastMovementMagnitude;
    float lastMovementMagnitude;
    float totalCount;
    float changeMovementAmount;
    public Image meanMovementSlider;
    public Image currentMovementSlider;
    public Image MeanMovementChangeImg;
    public Text meanMovementTxt;
    public Text currentMovementTxt;
    public Text totalMovementTxt;
    public Text movementMarkTxt;

    public GameObject gyroError;
    // Start is called before the first frame update
    void Start()
    {
        Loader();
        if (SystemInfo.supportsGyroscope)
        {
            gyroError.SetActive(false);
        }
        else
        {
            gyroError.SetActive(true);
        }
            savedLevels = 0;
    }

 
    void Loader()
    {
        

        for (int i = 0; i < levelList.Count; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                id= levelList[i].ToString()+j.ToString()+ " training";

                    
                    if (SaveManager.Instance.LoadSave(id)!=null)
                    {
                   // Debug.Log("XXXXXX" + id);

                        int totalTime = int.Parse(SaveManager.Instance.LoadSave(id )["totalTimeOfPlay"].Value);
                        countOfPlay = int.Parse(SaveManager.Instance.LoadSave(id)["countOfPlay"].Value);
                        totalmovements = float.Parse(SaveManager.Instance.LoadSave(id)["averageMovementMagnitude"].Value);
                        lastmovements = float.Parse(SaveManager.Instance.LoadSave(id)["totalMovementMagnitude"].Value);
                        totalCount = totalCount + countOfPlay;
                        savedLevels++;
                        /*totalTotalMovementMagnitude = totalTotalMovementMagnitude + totalmovements;*/
                        //if (meanTotalMovementMagnitude != 0)
                        //{
                        //    meanTotalMovementMagnitude = (meanTotalMovementMagnitude + totalmovements) / 2f;
                        //}
                        //else
                        //{
                        //    meanTotalMovementMagnitude = totalmovements;
                        //}
                        totalTotalMovementMagnitude = totalTotalMovementMagnitude + totalmovements*countOfPlay;
                        totalLastMovementMagnitude = totalLastMovementMagnitude + lastmovements;
                        Debug.Log(id+"_________________" + countOfPlay + " " + totalmovements + " " + lastmovements + " " + totalCount + " " + " " + meanTotalMovementMagnitude + " " + totalLastMovementMagnitude);
                        //if (isCountReport)
                        //{
                        //    GetComponent<Text>().text = ArabicFixer.Fix(countOfPlay.ToString() + " ", true, true);
                        //}

                        //if (isMarkReport)
                        //{


                        /*        }
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
                                }*/
                    }

                }
                   
            
            

        }
        if (savedLevels != 0)
        //if (ObscuredPrefs.GetInt(id + "Count") != 0)
        {
/*            int meanTime = totalTime / countOfPlay;
            int hour = meanTime / 3600;
            int min = (meanTime % 3600) / 60;
            int sec = (meanTime % 3600) % 60;*/

            meanMovementMagnitude = (float)(totalLastMovementMagnitude / (float)savedLevels) ;
            meanTotalMovementMagnitude = (float)(totalTotalMovementMagnitude / (float)totalCount);
            if (meanTotalMovementMagnitude != 0)
            {
                changeMovementAmount = (meanTotalMovementMagnitude - meanMovementMagnitude) / meanTotalMovementMagnitude;
            }
            else
            {
                changeMovementAmount = 0f;
            }
            

            float MaxofMove = 45;
            if (MaxofMove != 0)
            {
                Debug.Log("_________________" + meanMovementMagnitude + " " + meanTotalMovementMagnitude + " " + MaxofMove);
                meanMovementSlider.fillAmount = meanTotalMovementMagnitude / MaxofMove;
                meanMovementTxt.text = ArabicFixer.Fix(((float)System.Math.Round(meanTotalMovementMagnitude/10f,1)).ToString() + " متر ", true, true);


                currentMovementSlider.fillAmount = meanMovementMagnitude / MaxofMove;
                //currentMovementTxt.text = ArabicFixer.Fix(((float)System.Math.Round(meanMovementMagnitude/10f,1)).ToString() + " متر ", true, true);
                
                if (changeMovementAmount >= 0)
                {
                    currentMovementTxt.color = Color.blue;
                    MeanMovementChangeImg.color = Color.blue;
                    MeanMovementChangeImg.transform.Rotate (new Vector3(0,0,90));
                    currentMovementTxt.text = ArabicFixer.Fix("+"+((float)System.Math.Round(changeMovementAmount, 1)).ToString() + " % ", true, true);
                }
                else
                {
                    currentMovementTxt.color = Color.gray;
                    MeanMovementChangeImg.color = Color.gray;
                    MeanMovementChangeImg.transform.Rotate(new Vector3(0, 0, -90));
                    currentMovementTxt.text = ArabicFixer.Fix("-" + ((float)System.Math.Round(changeMovementAmount, 1)).ToString() + " % ", true, true);
                }

                if(meanMovementMagnitude >= 25)
                {
                    movementMarkTxt.text = ArabicFixer.Fix( "عالی", true, true);
                }
                else if(meanMovementMagnitude >= 15&& meanMovementMagnitude < 25)
                {
                    movementMarkTxt.text = ArabicFixer.Fix("خیلی خوب", true, true);
                }
                else if(meanMovementMagnitude < 15&& meanMovementMagnitude > 0)
                {
                    movementMarkTxt.text = ArabicFixer.Fix("خوب", true, true);
                }
                totalMovementTxt.text = ArabicFixer.Fix(((float)System.Math.Round(totalTotalMovementMagnitude / 10f, 1)).ToString() , true, true);
            }

            /*GetComponent<Text>().text = ArabicFixer.Fix(hour.ToString(), true, true) +
            ":" + ArabicFixer.Fix(min.ToString(), true, true) + "':" + ArabicFixer.Fix(sec.ToString(), true, true) + "''";*/
        }
        else
        {
            meanMovementSlider.fillAmount = 0f;
            currentMovementSlider.fillAmount = 0f;

            meanMovementTxt.text = ArabicFixer.Fix(("0").ToString() + " متر ", true, true);
            movementMarkTxt.text = ArabicFixer.Fix("-", true, true);

            currentMovementTxt.text = ArabicFixer.Fix(("0").ToString() + "%", true, true);

            currentMovementTxt.color = Color.green;
            MeanMovementChangeImg.color = Color.green;
            MeanMovementChangeImg.transform.Rotate(new Vector3(0, 0, 90));
            GetComponent<Text>().text = ArabicFixer.Fix("", true, true);
        }


    }
    public void PlayAlphabetSound(string levelName)
    {

        if (levelName == "B0")
        {
            //SoundManager.Instance.Play("B");
        }
        else if (levelName == "A0")
        {
            //SoundManager.Instance.Play("A");
        }
        else if (levelName == "Y0")
        {
            //SoundManager.Instance.Play("Y");
        }
        else if (levelName == "Z20")
        {
            //SoundManager.Instance.Play("Z2");
        }
        else if (levelName == "T20")
        {
            //SoundManager.Instance.Play("T2");
        }
        else if (levelName == "T0")
        {
            //SoundManager.Instance.Play("T");
        }
        else if (levelName == "P0")
        {
            //SoundManager.Instance.Play("P");
        }
        else if (levelName == "C30")
        {
            //SoundManager.Instance.Play("C3");
        }
        else if (levelName == "J0")
        {
            //SoundManager.Instance.Play("J");
        }
        else if (levelName == "Ch0")
        {
            //SoundManager.Instance.Play("Ch");
        }
        else if (levelName == "Hj0")
        {
            //SoundManager.Instance.Play("Hj");
        }
        else if (levelName == "Kh0")
        {
            //SoundManager.Instance.Play("Kh");
        }
        else if (levelName == "Zl0")
        {
            //SoundManager.Instance.Play("Zl");
        }
        else if (levelName == "D0")
        {
            //SoundManager.Instance.Play("D");
        }
        else if (levelName == "Zr0")
        {
            //SoundManager.Instance.Play("Zr");
        }
        else if (levelName == "R0")
        {
            //SoundManager.Instance.Play("R");
        }
        else if (levelName == "Zh0")
        {
            //SoundManager.Instance.Play("Zh");
        }
        else if (levelName == "S0")
        {
            //SoundManager.Instance.Play("S");
        }
        else if (levelName == "Sh0")
        {
            //SoundManager.Instance.Play("Sh");
        }
        else if (levelName == "Sd0")
        {
            //SoundManager.Instance.Play("Sd");
        }
        else if (levelName == "Zd0")
        {
            //SoundManager.Instance.Play("Zd");
        }
        else if (levelName == "T20")
        {
            //SoundManager.Instance.Play("T2");
        }
        else if (levelName == "Z20")
        {
            //SoundManager.Instance.Play("Z2");
        }
        else if (levelName == "Eyn0")
        {
            //SoundManager.Instance.Play("Eyn");
        }
        else if (levelName == "Ghyn0")
        {
            //SoundManager.Instance.Play("Ghyn");
        }
        else if (levelName == "F0")
        {
            //SoundManager.Instance.Play("F");
        }
        else if (levelName == "Gh0")
        {
            //SoundManager.Instance.Play("Gh");
        }
        else if (levelName == "K0")
        {
            //SoundManager.Instance.Play("K");
        }
        else if (levelName == "G0")
        {
            //SoundManager.Instance.Play("G");
        }
        else if (levelName == "L0")
        {
            //SoundManager.Instance.Play("L");
        }

        else if (levelName == "M0")
        {
            //SoundManager.Instance.Play("M");
        }
        else if (levelName == "N0")
        {
            //SoundManager.Instance.Play("N");
        }
        else if (levelName == "V0")
        {
            //SoundManager.Instance.Play("V");
        }
        else if (levelName == "H0")
        {
            //SoundManager.Instance.Play("H");
        }
        else if (levelName == "Practice0")
        {
            //SoundManager.Instance.Play("Practice");
        }
        else if (levelName == "Alpha")
        {
            //SoundManager.Instance.Play("Alpha");
        }
    }
    }
