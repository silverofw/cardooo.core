using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cardooo.core;

public class Demo_GameFSM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameFSM fsm = new GameFSM();
        var e = EntityMgr.Instance.Create<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        EntityMgr.Instance.Reset();
    }
}
