using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cardooo.core;

public class BagTest : MonoBehaviour
{
    int uid = 1000;
    int goldIndex = 10;
    [ContextMenu("Start")]
    void Start()
    {

        BagMgr.Instance.AddItem(uid, new GoldItem(uid, goldIndex,1));
    }

    [ContextMenu("Add")]
    void addGold()
    {
        BagMgr.Instance.AddItemByType(goldIndex, 1);
    }
}
