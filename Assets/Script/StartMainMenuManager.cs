using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using BizzyBeeGames;

public class StartMainMenuManager : MonoBehaviour
{
    public InputField inputField;
    public GameObject parentAuthen;

    private void Start()
    {
        parentAuthen.SetActive(false);
    }

    public void Type(string character)
    {
        if (inputField.text.Length < 6) { inputField.text += character; }

        
    }

    public void Remove()
    {
        if (inputField.text.Length > 0) { inputField.text = inputField.text.Remove(inputField.text.Length - 1); }

        
    }

    public void Confirm()
    {
        if (inputField.text == "109")
        {
            Debug.Log("welcom parents");
            Loading.Instance.ShowLoading("Parents Menu");
            SoundManager.Instance.Play("btn");
        }
        else
        {
            Debug.Log("its false");
        }
    }

    public void ShowParentAuthentication()
    {
        if (!parentAuthen.activeInHierarchy)
        {
            parentAuthen.SetActive(true);
            parentAuthen.GetComponent<CanvasGroup>().alpha = 0f;
            parentAuthen.GetComponent<CanvasGroup>().DOFade(1,0.4f);
            parentAuthen.transform.DOShakeScale(0.3f, 0.5f, 3, 90);

        }


        

    }
    public void HideParentAuthentication()
    {
        //parentAuthen.transform.DOScale(Vector3.zero, 0.1f).OnComplete(()=> { parentAuthen.SetActive(false); });
        parentAuthen.GetComponent<CanvasGroup>().DOFade(0, 0.1f);
        parentAuthen.transform.DOShakeScale(0.2f, 0.5f, 3, 90).OnComplete(() => { parentAuthen.SetActive(false); });

    }
}
