using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;

public class HiBuy : MonoBehaviour
{
    public GameObject[] PurchaseStaff;

    public void Set(int type)
    {
        foreach(GameObject i in PurchaseStaff)
        {
            i.SetActive(false);
        }

        if (type == 0)
        {
            PurchaseStaff[0].SetActive(true);
        }else if (type == 1)
        {
            PurchaseStaff[1].SetActive(true);
        }
        else if (type == 2)
        {
            PurchaseStaff[2].SetActive(true);
        }
        else if (type == 3)
        {
            PurchaseStaff[3].SetActive(true);
        }
        else if (type == 4)
        {
            PurchaseStaff[4].SetActive(true);
        }
        else if (type == 5)
        {
            PurchaseStaff[5].SetActive(true);
        }
        else if (type == 6)
        {
            PurchaseStaff[6].SetActive(true);
        }
        else if (type == 7)
        {
            PurchaseStaff[7].SetActive(true);
        }
        else if (type == 8)
        {
            PurchaseStaff[8].SetActive(true);
        }
        else if (type == 9)
        {
            PurchaseStaff[9].SetActive(true);
        }
    }

}
