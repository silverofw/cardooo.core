using UnityEngine;
using System.Collections.Generic;

namespace cardooo.core
{
    public class MonoGobjPool
    {
        string path = "";
        GameObject prefabGo = null;
        int count = 0;

        GameObject poolRoot = null;
        List<GameObject> poolObjList = new List<GameObject>();
        GameObject activeRoot = null;
        List<GameObject> activeObjList = new List<GameObject>();

        int curCount = 0;

        public MonoGobjPool(string path, GameObject prefab, GameObject root, int count = 1)
        {
            this.path = path;
            prefabGo = prefab;
            this.count = count;

            poolRoot = new GameObject($"[{path}][{count}] Pool");
            poolRoot.transform.SetParent(root.transform);
            activeRoot = new GameObject($"[{path}] Active");
            activeRoot.transform.SetParent(root.transform);

            ResizePool();
        }

        public GameObject Get(bool active = true, Transform parent = null, Vector3 pos = default, Vector3 rot = default)
        {
            if (poolObjList.Count == 0)
            {
                ResizePool();
            }

            GameObject go = poolObjList[0];
            poolObjList.RemoveAt(0);
            activeObjList.Add(go);
            go.transform.SetParent(parent == null ? activeRoot.transform : parent);
            go.transform.position = pos;
            go.transform.eulerAngles = rot;
            go.SetActive(active);
            return go;
        }

        public void Recycle(GameObject go)
        {
            if (!activeObjList.Contains(go))
            {
                DLog.LogError($"[MonoGobjPool][{go}] is not in activeObjList!");
            }

            go.SetActive(false);
            activeObjList.Remove(go);
            poolObjList.Add(go);
            go.transform.SetParent(poolRoot.transform);
        }

        void ResizePool()
        {
            DLog.Log($"[MonoGobjPool][ResizePool][{path}][{curCount + count}]");
            for (int i = 0; i < count; i++)
            {
                curCount++;
                poolObjList.Add(GameObject.Instantiate(prefabGo, poolRoot.transform));
            }

            poolRoot.name = $"[{path}][{poolObjList.Count}/{curCount}] Pool";
        }
    }
}