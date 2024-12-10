using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MainMenuChangingButton : MonoBehaviour,IPointerClickHandler
{
    public int col;
    public void OnPointerClick(PointerEventData eventData)
    {
        MainMenuExampleManager.Instance.ExampleElementButton(this.gameObject,col);
    }
}
