using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class NewBasment : MonoBehaviour
{
    public MeshRenderer[] type1, type2, type3;
    public Material[] A_material;
    public Material[] B_material;
    public Material[] P_material;

    public GameObject rotator;
    
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Contains("A"))
        {
            foreach(MeshRenderer i in type1)
            {
                i.material = A_material[0];
            }
            foreach (MeshRenderer i in type2)
            {
                i.material = A_material[1];
            }
            foreach (MeshRenderer i in type3)
            {
                i.material = A_material[2];
            }
        }

        if (SceneManager.GetActiveScene().name.Contains("B"))
        {
            foreach (MeshRenderer i in type1)
            {
                i.material = B_material[0];
            }
            foreach (MeshRenderer i in type2)
            {
                i.material = B_material[1];
            }
            foreach (MeshRenderer i in type3)
            {
                i.material = B_material[2];
            }
        }

        if (SceneManager.GetActiveScene().name.Contains("P"))
        {
            foreach (MeshRenderer i in type1)
            {
                i.material = P_material[0];
            }
            foreach (MeshRenderer i in type2)
            {
                i.material = P_material[1];
            }
            foreach (MeshRenderer i in type3)
            {
                i.material = P_material[2];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        rotator.transform.Rotate(new Vector3(0, 0, 2) * Time.deltaTime * 10);
    }
}
