using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard : MonoBehaviour
{
    public static BlackBoard Inst = null;

    public Transform cube;

    // Start is called before the first frame update
    void Start()
    {
        Inst = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
