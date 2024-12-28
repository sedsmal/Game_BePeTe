using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExitBtn : MonoBehaviour,IPointerClickHandler
{
    Animator animator;
    public GameObject extePopup;
    public void OnPointerClick(PointerEventData eventData)
    {
        animator.ResetTrigger("cancelExit");
        animator.SetTrigger("playExit");
        ShowPopup();
    }

    // Start is called before the first frame update
    void Start()
    {
        extePopup.SetActive(false);
        animator = GetComponent<Animator>();
    }
    public void ShowPopup()
    {
        extePopup.SetActive(true);
    }
    public void HidePopup()
    {
        extePopup.SetActive(false);
        animator.SetTrigger("cancelExit");
        animator.ResetTrigger("playExit");
    }
    public void ExiteGame()
    {
        Application.Quit();
    }



}
