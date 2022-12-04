using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteMonkeyView : MonoBehaviour
{
    public Button start;
    public InputField monkeyCountText;
    public InputField typeStringText;
    public Text logText;

    public InfiniteMonkey infiniteMonkey;

    // Start is called before the first frame update
    void Start()
    {
        start.onClick.RemoveAllListeners();
        start.onClick.AddListener(startType);
    }

    // Update is called once per frame
    void startType()
    {
        int monkeyCount = int.Parse(monkeyCountText.text);
        string typeString = typeStringText.text;
        infiniteMonkey.MokeyType(monkeyCount, typeString, report);
    }

    void report(string log)
    {
        logText.text = log;
    }
}
