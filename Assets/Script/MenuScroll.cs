using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScroll : MonoBehaviour
{

    public ScrollRect scrollRect;
    public RectTransform viewPortRectTransform;
    public RectTransform contentPanelTransform;
    public HorizontalLayoutGroup hlg;

    public RectTransform[] itemList;
    public RectTransform item;
    float lenght;
    Vector2 oldVelocity;
    bool isUpdated;
    private void Start()
    {
        isUpdated = false;
        oldVelocity = Vector2.zero;
        int itemToAdd = Mathf.CeilToInt(viewPortRectTransform.rect.width / (itemList[0].rect.width + hlg.spacing));
        for (int i = 0; i < itemToAdd; i++)
        {
            RectTransform rt = Instantiate(itemList[i % itemList.Length], contentPanelTransform);
            rt.SetAsLastSibling();
        }

        for (int i = 0; i < itemToAdd; i++)
        {
            int num = itemList.Length - i - 1;
            while (num < 0)
            {
                num += itemList.Length;
            }
            RectTransform rt = Instantiate(itemList[num], contentPanelTransform);
            rt.SetAsFirstSibling();
        }

        contentPanelTransform.localPosition = new Vector3((0 - (itemList[0].rect.width + hlg.spacing) * itemToAdd),
            contentPanelTransform.localPosition.y, contentPanelTransform.localPosition.z);


    }

    private void Update()
    {
//        lenght = itemList.Length * (itemList[0].rect.width + hlg.spacing);
        //if (isUpdated)
        //{
        //    isUpdated = false;
        //    scrollRect.velocity = oldVelocity;
        //}
//        Debug.Log(contentPanelTransform.localPosition.x);

        if (contentPanelTransform.localPosition.x > 0)
        {
            Canvas.ForceUpdateCanvases();
            //oldVelocity = scrollRect.velocity;
            contentPanelTransform.localPosition -= new Vector3(itemList.Length * (itemList[0].rect.width + hlg.spacing), 0, 0);
            isUpdated = true;
            //Canvas.ForceUpdateCanvases();
        }

        if (contentPanelTransform.localPosition.x < 0 - (itemList.Length * (itemList[0].rect.width + hlg.spacing)))
        {
            Canvas.ForceUpdateCanvases();
            //oldVelocity = scrollRect.velocity;
            contentPanelTransform.localPosition += new Vector3(itemList.Length * (itemList[0].rect.width + hlg.spacing), 0, 0);
            isUpdated = true;
            //Canvas.ForceUpdateCanvases();
        }



    }

}
