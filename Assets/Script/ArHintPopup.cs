using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArHintPopup : MonoBehaviour
{
    public GameObject deprecatedImages;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            using (AndroidJavaClass versionClass = new AndroidJavaClass("android.os.Build$VERSION"))
            {
                int apiLevel = versionClass.GetStatic<int>("SDK_INT");
                if (apiLevel > 31)
                {
                    deprecatedImages.SetActive(false);
                    // Do something for API level > 31
                }
                else
                {
                    // Do something for API level <= 31
                    deprecatedImages.SetActive(true);

                }
            }
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
