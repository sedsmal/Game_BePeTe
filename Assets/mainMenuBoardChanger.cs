using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.Storage;
using UnityEngine.UI;

public class mainMenuBoardChanger : MonoBehaviour
{
    Image img;
    RawImage rimg;
    SpriteRenderer sr;
    public Sprite[] backgrounds;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        img = GetComponent<Image>();
        rimg = GetComponent<RawImage>();

        if (img!=null)
            img.sprite = backgrounds[ObscuredPrefs.GetInt("ActiBkg")];

        if (sr != null)
            sr.sprite = backgrounds[ObscuredPrefs.GetInt("ActiBkg")];

        

    }
    public void changeBoard()
    {
        int activeBoard = ObscuredPrefs.GetInt("ActiBkg");

        if (activeBoard < backgrounds.Length-1)
        {
            activeBoard++;

            if (img != null)
                img.sprite = backgrounds[activeBoard];

            if (sr != null)
                sr.sprite = backgrounds[activeBoard];
        }
        else
        {
            activeBoard = 0;

            if (img != null)
                img.sprite = backgrounds[activeBoard];

            if (sr != null)
                sr.sprite = backgrounds[activeBoard];
        }

        ObscuredPrefs.SetInt("ActiBkg", activeBoard);
    }
}
