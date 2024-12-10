using UnityEngine;
using UnityEngine.UI;

public class DeviceID : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    #if UNITY_IPHONE

        GetComponent<Text>().text = UIDevice.identifierForVendor;

    #endif

    #if UNITY_ANDROID

        GetComponent<Text>().text = SystemInfo.deviceUniqueIdentifier;

    #endif

    #if UNITY_EDITOR

                GetComponent<Text>().text = "Editor";
    #endif

    }

}
