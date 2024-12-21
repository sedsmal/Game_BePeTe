using System.Collections;
using UnityEngine;
using BizzyBeeGames;
using CodeStage.AntiCheat.Storage;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuExampleManager : SingletonComponent<MainMenuExampleManager>
{
    //public GameObject[] elements;
    public GameObject[] prefabs;
    [HideInInspector] public GameObject activeElement;
    GameObject changePopup;
    public GameObject cloadParticle;
    private int activeCol;

    private void Start()
    {

        changePopup = GameObject.Find("ChangingPopup");
        changePopup.SetActive(false);

        if (ObscuredPrefs.GetString("MenuExample")!="") { LoadExamples(); }

    }

    public void SetElement(int i)
    {
        int j = 0;
        for(int a = 0; a < transform.childCount; a++)
        {
            if (activeElement.name.Contains(transform.GetChild(a).gameObject.name)&&a==activeCol)
            {
                string name = transform.GetChild(a).gameObject.name;
                GameObject pa = Instantiate(cloadParticle, transform.GetChild(a), false);

                //GameObject n = Instantiate(prefabs[i], this.transform, false);
                //n.GetComponent<MainMenuChangingButton>().col = activeCol;

                //n.transform.position = transform.GetChild(a).gameObject.transform.position;
                //n.name = n.name.Remove(n.name.Length - 7);
                //Destroy(transform.GetChild(a).gameObject);
                //n.transform.SetSiblingIndex(a);
                //StartCoroutine(Save());
                //changePopup.SetActive(false);
                StartCoroutine(SetElementIe(i, a,pa));
            }

        }
    }

    IEnumerator SetElementIe(int i , int a,GameObject pa)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject n = Instantiate(prefabs[i], this.transform, false);
        n.GetComponent<MainMenuChangingButton>().col = activeCol;
        pa.transform.SetParent(n.transform);
        n.transform.position = transform.GetChild(a).gameObject.transform.position;
        n.transform.GetChild(0).GetChild(1).DOShakeRotation(2, 20, 4, 90);
        pa.transform.position = n.transform.position;
        n.name = n.name.Remove(n.name.Length - 7);
        Destroy(transform.GetChild(a).gameObject);
        n.transform.SetSiblingIndex(a);
        StartCoroutine(Save());
        changePopup.SetActive(false);
    }

    public void ExampleElementButton(GameObject thisGo,int activeColl)
    {
        changePopup.SetActive(true);
        changePopup.transform.DOShakeScale(0.3f, 0.05f, 3, 90);
        changePopup.GetComponent<Canvas>().sortingOrder++;
        activeElement = thisGo;
        activeCol = activeColl;
    }
    public void HideChangingPopup()
    {
        changePopup.transform.DOShakeScale(0.2f, 0.05f, 3, 90).OnComplete(() => { changePopup.SetActive(false); });
        
    }
    IEnumerator Save()
    {
        yield return new WaitForSeconds(0.5f);
        SaveExamples();
    }
    private void SaveExamples()
    {
        string exampleList = "";
        for (int a = 0; a < transform.childCount; a++)
        {
            Debug.Log(transform.GetChild(a).gameObject.name + a);
            exampleList += transform.GetChild(a).gameObject.name + "/";
        }
        //    foreach (GameObject k in elements)
        //{
        //    exampleList += k.name+"/";
        //}
        Debug.Log(exampleList);
        ObscuredPrefs.SetString("MenuExample", exampleList);
    }
    private void LoadExamples()
    {
        string exampleList = ObscuredPrefs.GetString("MenuExample");
        Debug.Log(exampleList);
        string[] names = exampleList.Split("/");
        Debug.Log(names.Length);
        int i = 0;
        //foreach (string n in names)
        //{
        //    Debug.Log(n);
        //}
        foreach (string n in names)
        {
            //Debug.Log(i);
            foreach (GameObject g in prefabs)
            {
                //Debug.Log("@; "+i);
                //Debug.Log("@@; " + n);
                //Debug.Log("@@@; " + g.name);
                if (g.name == n)
                {
                    Debug.Log("N; "+ n+" "+i);
                    GameObject S = Instantiate(g, this.transform, false);
                    S.transform.position = transform.GetChild(i).position;
                    S.name = S.name.Remove(S.name.Length - 7);
                    S.transform.SetAsLastSibling();
                    S.GetComponent<MainMenuChangingButton>().col = i;
                    DestroyImmediate(transform.GetChild(i).gameObject,true);
                    S.transform.SetSiblingIndex(i);
                    
                    break;
                }
            }
            i++;
        }
    }

       
    

}
