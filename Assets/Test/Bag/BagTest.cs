using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cardooo.core;

public class BagTest : MonoBehaviour
{
    public int uid = 1000;
    public int goldIndex = 10;
    [ContextMenu("Start")]
    void Start()
    {
        BagMgr.Instance.AddItem(new GoldItem(uid, goldIndex, 1));
    }

    [ContextMenu("Add")]
    void Add()
    {
        BagMgr.Instance.AddItemByType(goldIndex, 1);
    }

    [ContextMenu("ClearAll")]
    void ClearAll()
    {
        BagMgr.Instance.ClearAll();
    }
}
