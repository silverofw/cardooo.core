using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning : MonoBehaviour
{
    public Vector3 rott = new Vector3(0, 1, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rot + rott * Time.deltaTime);
    }
}
