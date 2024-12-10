using UnityEngine;
using UnityEngine.UI;
using CodeStage.AntiCheat.Storage;

public class MobileNumberText : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = ObscuredPrefs.GetString("_N");
    }


}
