using UnityEngine;
using cardooo.core;
using System;

public class Demo_GameFSM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameFSM fsm = new GameFSM();
        Entity e = EntityMgr.Instance.Create<Entity>();
        EntityMgr.Instance.DeleteEntity(e);

        Debug.Log($"{e.Id}");
    }

    // Update is called once per frame
    void Update()
    {
        EventMgr.Instance.MainHandler.Send(0);
    }

    private void OnDestroy()
    {
        EntityMgr.Instance.Reset();
        EntitySystemMgr.Instance.Reset();
    }
}
